using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SiLadhida.API.DTOs;
using SiLadhida.API.Common;
using SiLadhida.API.Services.Interfaces;

namespace SiLadhida.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly ILogger<OrderController> _logger;

    public OrderController(
        IOrderService service,
        ILogger<OrderController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Fetching all orders");

        var data = await _service.GetAllAsync();

        return Ok(
            ApiResponse<object>.SuccessResponse(
                data,
                "Berhasil mengambil data pesanan"
            )
        );
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto? dto)
    {
        if (dto == null)
        {
            return BadRequest(
                ApiResponse<object>.ErrorResponse(
                    "Data pesanan tidak valid"
                )
            );
        }

        _logger.LogInformation("Creating new order for customer {CustomerName}", dto.NamaPemesan);

        var result = await _service.CreateAsync(dto);

        return Ok(
            ApiResponse<object>.SuccessResponse(
                result,
                "Pesanan berhasil dibuat"
            )
        );
    }

    [Authorize]
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        [FromBody] UpdateStatusDto? dto)
    {
        if (dto == null)
        {
            return BadRequest(
                ApiResponse<object>.ErrorResponse(
                    "Data status tidak valid"
                )
            );
        }

        _logger.LogInformation("Updating status for order {OrderId}", id);

        var result = await _service.UpdateStatusAsync(id, dto);

        if (result == null)
        {
            _logger.LogWarning("Order not found with ID {OrderId}", id);

            return NotFound(
                ApiResponse<object>.ErrorResponse(
                    "Pesanan tidak ditemukan"
                )
            );
        }

        return Ok(
            ApiResponse<object>.SuccessResponse(
                result,
                "Status pesanan berhasil diperbarui"
            )
        );
    }
}