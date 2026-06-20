using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SiLadhida.API.Common;
using SiLadhida.Application.Interfaces;
using SiLadhida.API.DTOs;
using SiLadhida.API.DTOs.Responses;
using AutoMapper;

namespace SiLadhida.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public ProductController(
        IProductService service,
        IMapper mapper,
        ILogger<ProductController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [Authorize(Roles = $"{Roles.Admin}, {Roles.Kasir}")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Get all products by {User}", User.Identity?.Name);

        var products = await _service.GetAllAsync();
        var result = _mapper.Map<List<ProductResponseDto>>(products);

        return Ok(ApiResponse<object>.SuccessResponse(
            result,
            "Berhasil mengambil data produk"
        ));
    }

    [Authorize(Roles = $"{Roles.Admin}, {Roles.Kasir}")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("Get product: Id={Id}, User={User}",
            id, User.Identity?.Name);

        var product = await _service.GetByIdAsync(id);
        var result = _mapper.Map<ProductResponseDto>(product);

        return Ok(ApiResponse<object>.SuccessResponse(
            result,
            "Berhasil mengambil produk"
        ));
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        _logger.LogInformation("Create Product: {Nama}, Harga={Harga}, Stock={Stock}, User={User}",
            dto.Nama, dto.Harga, dto.Stock, User.Identity?.Name);

        var product = await _service.CreateAsync(dto.Nama, dto.Harga, dto.Stock);
        var result = _mapper.Map<ProductResponseDto>(product);

        return Ok(ApiResponse<object>.SuccessResponse(
            result,
            "Produk berhasil dibuat"
        ));
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductDto dto)
    {
        _logger.LogInformation("Update Product: Id={Id}, Nama={Nama}, User={User}",
            id, dto.Nama, User.Identity?.Name);

        var product = await _service.UpdateAsync(id, dto.Nama, dto.Harga, dto.Stock);
        var result = _mapper.Map<ProductResponseDto>(product);

        return Ok(ApiResponse<object>.SuccessResponse(
            result,
            "Produk berhasil diperbarui"
        ));
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogWarning("Delete Product: Id={Id}, User={User}",
            id, User.Identity?.Name);

        await _service.DeleteAsync(id);

        return Ok(ApiResponse<object>.SuccessResponse(
            null,
            "Produk berhasil dihapus"
        ));
    }

}