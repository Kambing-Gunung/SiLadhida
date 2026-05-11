using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SiLadhida.API.Data;
using SiLadhida.API.DTOs;
using SiLadhida.Core.Entities;
using SiLadhida.Core.Enums;
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
        public IActionResult Create([FromBody] CreateOrderDto dto)
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
                    var produk = _context.Produk.Find(itemDto.ProdukId);

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

                _context.SaveChanges();

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
        public IActionResult GetAll()
        {
            var data = _context.Pesanan
                .Include(p => p.Items)
                .ThenInclude(i => i.Produk) 
                .ToList();
            return Ok(data);
        }

        // UPDATE STATUS (STATE-BASED)
        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
        {
            var order = _context.Pesanan.Find(id);

            if (order == null)
                return NotFound("Pesanan tidak ditemukan");

            if (!_service.IsValidTransition(order.StatusSekarang, dto.StatusBaru))
                return BadRequest("Transisi status tidak valid");

            order.StatusSekarang = dto.StatusBaru;
            _context.SaveChanges();

            _logger.LogInformation(
                "Status pesanan {OrderId} berubah menjadi {Status}",
                order.Id,
                order.StatusSekarang
            );

            return Ok(order);
        }
    }
}