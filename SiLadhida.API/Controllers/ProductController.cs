using Microsoft.AspNetCore.Mvc;
using SiLadhida.API.Common;
using SiLadhida.API.Services.Interfaces;
using SiLadhida.API.DTOs;
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

    // GET ALL
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

    // GET BY ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.GetByIdAsync(id);

        if (data == null)
        {
            return NotFound(
                ApiResponse<object>.ErrorResponse(
                    "Produk tidak ditemukan"
                )
            );
        }

        return Ok(
            ApiResponse<object>.SuccessResponse(
                data,
                "Berhasil mengambil produk"
            )
        );
    }

    // CREATE
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

        _logger.LogInformation(
            "Creating new product: {ProductName}",
            produk.Nama
        );

        var result = await _service.CreateAsync(produk);

        return Ok(
            ApiResponse<object>.SuccessResponse(
                result,
                "Produk berhasil dibuat"
            )
        );
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateProductDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);

        if (result == null)
        {
            return NotFound(
                ApiResponse<object>.ErrorResponse(
                    "Produk tidak ditemukan"
                )
            );
        }

        return Ok(
            ApiResponse<object>.SuccessResponse(
                result,
                "Produk berhasil diperbarui"
            )
        );
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
        {
            return NotFound(
                ApiResponse<object>.ErrorResponse(
                    "Produk tidak ditemukan"
                )
            );
        }

        return Ok(
            ApiResponse<object>.SuccessResponse(
                null,
                "Produk berhasil dihapus"
            )
        );
    }
}