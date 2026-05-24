using Microsoft.AspNetCore.Mvc;
using SiLadhida.API.Common;
using SiLadhida.API.Services.Interfaces;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    private readonly ILogger<ProductController> _logger;

    public ProductController(
        IProductService service,
        ILogger<ProductController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Fetching all products");

        var data = await _service.GetAllAsync();

        return Ok(
            ApiResponse<object>.SuccessResponse(
                data,
                "Berhasil mengambil data produk"
            )
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Produk produk)
    {
        if (produk == null)
        {
            return BadRequest(
                ApiResponse<object>.ErrorResponse(
                    "Data produk tidak valid"
                )
            );
        }

        _logger.LogInformation("Creating new product: {ProductName}", produk.Nama);

        var result = await _service.CreateAsync(produk);

        return Ok(
            ApiResponse<object>.SuccessResponse(
                result,
                "Produk berhasil dibuat"
            )
        );
    }
}
