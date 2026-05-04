using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public OrderController(AppDbContext context, PesananService service)
        {
            _context = context;
            _service = service;
        }

        // CREATE ORDER
        [HttpPost]
        public IActionResult Create([FromBody] CreateOrderDto dto)
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

                var item = new OrderItem
                {
                    ProdukId = produk.Id,
                    Quantity = itemDto.Quantity,
                    Harga = produk.Harga
                };

                items.Add(item);
            }

            order.Items = items;

            foreach (var item in items)
            {
                item.Pesanan = order;
            }

            order.TotalHarga = _service.HitungTotal(items);

            _context.Pesanan.Add(order);
            _context.SaveChanges();

            return Ok(order);
        }

        // GET ALL
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _context.Pesanan
                .Include(p => p.Items)
                .ThenInclude(i => i.Produk) // 🔥 INI TAMBAHAN
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

            return Ok(order);
        }
    }
}