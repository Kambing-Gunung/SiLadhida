using Microsoft.AspNetCore.Mvc;
using SiLadhida.API.Data;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _context.Produk.ToList();
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(Produk produk)
        {
            _context.Produk.Add(produk);
            _context.SaveChanges();
            return Ok(produk);
        }
    }
}