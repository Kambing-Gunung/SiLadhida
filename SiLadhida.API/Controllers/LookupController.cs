using Microsoft.AspNetCore.Mvc;
using SiLadhida.Core.Services;

namespace SiLadhida.API.Controllers
{
    [ApiController]
    [Route("api/lookup")]
    public class LookupController : ControllerBase
    {
        private readonly ProdukLookupService _service;

        public LookupController(ProdukLookupService service)
        {
            _service = service;
        }

        [HttpGet("{kode}")]
        public IActionResult GetProduk(string kode)
        {
            var nama = _service.GetNamaProduk(kode);

            if (nama == null)
                return NotFound("Kode produk tidak ditemukan");

            return Ok(new
            {
                kode,
                namaProduk = nama
            });
        }
    }
}