using Microsoft.AspNetCore.Mvc;
using SiLadhida.API.DTOs;
using SiLadhida.API.Common;
using SiLadhida.API.Services.Interfaces;

namespace SiLadhida.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();

        return Ok(
            ApiResponse<object>.SuccessResponse(
                data,
                "Berhasil mengambil data pesanan"
            )
        );
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateOrderDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(
            ApiResponse<object>.SuccessResponse(
                result,
                "Pesanan berhasil dibuat"
            )
        );
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        [FromBody] UpdateStatusDto dto)
    {
        var result = await _service.UpdateStatusAsync(id, dto);

        if (result == null)
            return NotFound("Pesanan tidak ditemukan");

        return Ok(
            ApiResponse<object>.SuccessResponse(
                result,
                "Status pesanan berhasil diperbarui"
            )
        );
    }
}