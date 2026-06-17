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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order =
            await _service.GetByIdAsync(id);

        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [Authorize]
    [HttpPost("{id}/items")]
    public async Task<IActionResult> AddItem(int id, AddOrderItemDto dto)
    {
        var order =
            await _service.AddItemAsync(
                id,
                dto.ProductId,
                dto.Quantity);

        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [Authorize]
    [HttpDelete("{id}/items/{productId}")]
    public async Task<IActionResult> RemoveItem(int id, int productId)
    {
        var order =
            await _service.RemoveItemAsync(
                id,
                productId);

        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("{id}/items/{productId}/increase")]
    public async Task<IActionResult> IncreaseItem(
        int id,
        int productId,
        UpdateItemQuantityDto dto)
    {
        var order =
            await _service.IncreaseItemAsync(
                id,
                productId,
                dto.Quantity);

        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("{id}/items/{productId}/decrease")]
    public async Task<IActionResult> DecreaseItem(
        int id,
        int productId,
        UpdateItemQuantityDto dto)
    {
        var order =
            await _service.DecreaseItemAsync(
                id,
                productId,
                dto.Quantity);

        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpDelete("{id}/items")]
    public async Task<IActionResult> ClearItems(int id)
    {
        var order =
            await _service.ClearItemsAsync(id);

        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("{id}/pay")]
    public async Task<IActionResult> Pay(int id)
    {
        var order =
            await _service.PayOrderAsync(id);

        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        var order =
            await _service.CancelOrderAsync(id);

        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var order =
            await _service.CompleteOrderAsync(id);

        if (order is null)
            return NotFound();

        return Ok(order);
    }
}