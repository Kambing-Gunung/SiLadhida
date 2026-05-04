using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create(CreateOrderDto dto)
        {
            var order = new Pesanan
            {
                NamaPemesan = dto.NamaPemesan,
                NamaKue = dto.NamaKue,
                StatusSekarang = _service.GetInitialStatus()
            };

            _context.Pesanan.Add(order);
            _context.SaveChanges();

            return Ok(order);
        }

        // GET ALL
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _context.Pesanan.ToList();
            return Ok(data);
        }

        // UPDATE STATUS (STATE-BASED)
        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, UpdateStatusDto dto)
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