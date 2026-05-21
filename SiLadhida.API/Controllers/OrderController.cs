using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiLadhida.API.Data;
using SiLadhida.API.DTOs;
using SiLadhida.Core.Entities;
using SiLadhida.Core.Services;

namespace SiLadhida.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PesananService _service;

        private readonly ILogger<OrderController> _logger;

        public OrderController(AppDbContext context, PesananService service, ILogger<OrderController> logger)
        {
            _context = context;
            _service = service;
            _logger = logger;
        }

        // CREATE ORDER
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var order = new Pesanan
                {
                    NamaPemesan = dto.NamaPemesan,
                    StatusSekarang = _service.GetInitialStatus()
                };

                var items = new List<OrderItem>();

                foreach (var itemDto in dto.Items)
                {
                    var produk = await _context.Produk.FindAsync(itemDto.ProdukId);

                    if (produk == null)
                        return BadRequest($"Produk ID {itemDto.ProdukId} tidak ditemukan");

                    if (itemDto.Quantity > produk.Stock)
                    {
                        _logger.LogWarning(
                            "Stock tidak mencukupi untuk produk {NamaProduk}",
                            produk.Nama
                        );

                        return BadRequest(
                            $"Stock produk {produk.Nama} tidak mencukupi. " +
                            $"Stock tersedia: {produk.Stock}"
                        );
                    }

                    var item = new OrderItem
                    {
                        ProdukId = produk.Id,
                        Quantity = itemDto.Quantity,
                        Harga = produk.Harga
                    };

                    produk.Stock -= itemDto.Quantity;

                    items.Add(item);
                }

                order.Items = items;

                foreach (var item in items)
                {
                    item.Pesanan = order;
                }

                order.TotalHarga = _service.HitungTotal(items);

                _context.Pesanan.Add(order);

                // throw new Exception("Simulasi gagal");

                await _context.SaveChangesAsync();

                transaction.Commit();

                _logger.LogInformation(
                    "Pesanan berhasil dibuat oleh {NamaPemesan} dengan total {TotalHarga}", 
                    order.NamaPemesan, 
                    order.TotalHarga
                );

                return Ok(order);
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                _logger.LogError(ex, "Terjadi error saat membuat pesanan");

                return StatusCode(500,
                    $"Terjadi kesalahan: {ex.Message}");
            }

        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Pesanan
                .Include(p => p.Items)
                .ThenInclude(i => i.Produk) 
                .ToListAsync();
            return Ok(data);
        }

        // UPDATE STATUS (STATE-BASED)
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            var order = await _context.Pesanan.FindAsync(id);

            if (order == null)
                return NotFound("Pesanan tidak ditemukan");

            if (!_service.IsValidTransition(order.StatusSekarang, dto.Trigger))
                return BadRequest("Transisi status tidak valid");

            order.StatusSekarang = _service.GetNextState(order.StatusSekarang, dto.Trigger);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Status pesanan {OrderId} | trigger {Trigger} | menjadi {Status}",
                order.Id,
                dto.Trigger,
                order.StatusSekarang
            );

            return Ok(order);
        }
    }
}