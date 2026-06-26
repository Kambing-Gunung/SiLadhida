using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiLadhida.API.Common;
using SiLadhida.API.DTOs;
using SiLadhida.API.DTOs.Responses;
using SiLadhida.API.FactoryMethod.Factory;
using SiLadhida.API.FactoryMethod.Product;
using SiLadhida.Core.Enums;
using SiLadhida.Application.Interfaces;

namespace SiLadhida.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    IPay pay;
    PaymentFactory paymentFactory;

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
    [HttpPatch("{id}/cashpayment")]
    public async Task<IActionResult> CashPayment(int id)
    {
        _logger.LogInformation(
            "Update Status: OrderId={OrderId}, Trigger={Trigger}, User={User}",
            id, StateTrigger.PembayaranDikonfirmasi, User.Identity?.Name);

        paymentFactory = new PaymentOfflineFactory();
        pay = paymentFactory.CreatePay();

        var order = await pay.Payment(id, StateTrigger.PembayaranDikonfirmasi, _service);
        var result = _mapper.Map<OrderResponseDto>(order);

        return Ok(ApiResponse<object>.SuccessResponse(
            result,
            "Status order berhasil diperbarui"
        ));
    }

    [Authorize(Roles = $"{Roles.Admin}, {Roles.Kasir}")]
    [HttpPatch("{id}/qrispayment")]
    public async Task<IActionResult> QrisPayment(int id)
    {
        _logger.LogInformation(
            "Update Status: OrderId={OrderId}, Trigger={Trigger}, User={User}",
            id, StateTrigger.PembayaranDikonfirmasi, User.Identity?.Name);

        paymentFactory = new PaymentOnlineFactory();
        pay = paymentFactory.CreatePay();

        var order = await pay.Payment(id, StateTrigger.PembayaranDikonfirmasi, _service);
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

    [Authorize(Roles = $"{Roles.Admin}, {Roles.Kasir}")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation(
            "Delete Order: OrderId={OrderId}, User={User}",
            id, User.Identity?.Name);

        await _service.DeleteAsync(id);

        // Mengembalikan response sukses tanpa data spesifik
        return Ok(ApiResponse<object>.SuccessResponse(
            null,
            "Data pesanan berhasil dihapus secara permanen"
        ));
    }

}