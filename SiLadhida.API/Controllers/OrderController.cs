using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SiLadhida.API.DTOs;
using SiLadhida.API.Common;
using SiLadhida.Application.Interfaces;
using AutoMapper;
using SiLadhida.API.DTOs.Responses;

namespace SiLadhida.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public OrderController(
        IOrderService service,
        IMapper mapper,
        ILogger<OrderController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [Authorize(Roles = $"{Roles.Admin}, {Roles.Kasir}")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Get all orders by {User}", User.Identity?.Name);

        var orders = await _service.GetAllAsync();
        var result = _mapper.Map<List<OrderResponseDto>>(orders);

        return Ok(ApiResponse<object>.SuccessResponse(
            result,
            "Berhasil mengambil data pesanan"
        ));
    }

    [Authorize(Roles = $"{Roles.Admin}, {Roles.Kasir}")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("Get order: OrderId={OrderId}, User={User}",
            id, User.Identity?.Name);

        var order = await _service.GetByIdAsync(id);
        var result = _mapper.Map<OrderResponseDto>(order);

        return Ok(ApiResponse<object>.SuccessResponse(
            result,
            "Berhasil mengambil order"
        ));
    }

    [Authorize(Roles = $"{Roles.Admin}, {Roles.Kasir}")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        _logger.LogInformation("Create Order by {User} for {NamaPemesan}",
            User.Identity?.Name, dto.NamaPemesan);

        var order = await _service.CreateAsync(dto.NamaPemesan);
        var result = _mapper.Map<OrderResponseDto>(order);

        return Ok(ApiResponse<object>.SuccessResponse(
            result,
            "Order berhasil dibuat"
        ));
    }

    [Authorize(Roles = $"{Roles.Admin}, {Roles.Kasir}")]
    [HttpPost("{id}/items")]
    public async Task<IActionResult> AddItem(int id, AddOrderItemDto dto)
    {
        _logger.LogInformation(
            "Add Item: OrderId={OrderId}, ProductId={ProductId}, Qty={Qty}, User={User}",
            id, dto.ProductId, dto.Quantity, User.Identity?.Name);

        var order = await _service.AddItemAsync(id, dto.ProductId, dto.Quantity);
        var result = _mapper.Map<OrderResponseDto>(order);

        return Ok(ApiResponse<object>.SuccessResponse(
            result,
            "Item berhasil ditambahkan"
        ));
    }

    [Authorize(Roles = $"{Roles.Admin}, {Roles.Kasir}")]
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateStatusDto dto)
    {
        _logger.LogInformation(
            "Update Status: OrderId={OrderId}, Trigger={Trigger}, User={User}",
            id, dto.Trigger, User.Identity?.Name);

        var order = await _service.UpdateStatusAsync(id, dto.Trigger);
        var result = _mapper.Map<OrderResponseDto>(order);

        return Ok(ApiResponse<object>.SuccessResponse(
            result,
            "Status order berhasil diperbarui"
        ));
    }

    [Authorize(Roles = $"{Roles.Admin}, {Roles.Kasir}")]
    [HttpPatch("{id}/items/{productId}")]
    public async Task<IActionResult> UpdateItem(
        int id,
        int productId,
        UpdateItemQuantityDto dto)
    {
        _logger.LogInformation(
            "Update Item: OrderId={OrderId}, ProductId={ProductId}, NewQty={Qty}, User={User}",
            id, productId, dto.Quantity, User.Identity?.Name);

        var order = await _service.UpdateItemQuantityAsync(id, productId, dto.Quantity);
        var result = _mapper.Map<OrderResponseDto>(order);

        return Ok(ApiResponse<object>.SuccessResponse(
            result,
            "Quantity item berhasil diperbarui"
        ));
    }
}