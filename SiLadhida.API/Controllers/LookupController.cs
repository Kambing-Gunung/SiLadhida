using Microsoft.AspNetCore.Mvc;
using SiLadhida.API.Common;
using SiLadhida.Core.Services;

namespace SiLadhida.API.Controllers;

[ApiController]
[Route("api/lookup")]
public class LookupController : ControllerBase
{
    private readonly ProdukLookupService _service;
    private readonly ILogger<LookupController> _logger;

    public LookupController(
        ProdukLookupService service,
        ILogger<LookupController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("{kode}")]
    public IActionResult GetProduk(string kode)
    {
        if (string.IsNullOrWhiteSpace(kode))
        {
            return BadRequest(
                ApiResponse<object>.ErrorResponse(
                    "Kode produk tidak boleh kosong"
                )
            );
        }

        _logger.LogInformation("Looking up product with code {ProductCode}", kode);

        var nama = _service.GetNamaProduk(kode);

        if (nama == null)
        {
            _logger.LogWarning("Product not found with code {ProductCode}", kode);

            return NotFound(
                ApiResponse<object>.ErrorResponse(
                    "Kode produk tidak ditemukan"
                )
            );
        }

        return Ok(
            ApiResponse<object>.SuccessResponse(
                new
                {
                    kode,
                    namaProduk = nama
                },
                "Produk ditemukan"
            )
        );
    }
}