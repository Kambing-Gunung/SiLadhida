using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SiLadhida.API.Data;
using SiLadhida.API.DTOs;
using SiLadhida.API.DTOs.Responses;
using SiLadhida.API.Repositories.Interfaces;
using SiLadhida.API.Services.Interfaces;
using SiLadhida.Core.Entities;
using SiLadhida.Core.Services;
using SiLadhida.API.Factories.Interfaces;

namespace SiLadhida.API.Services.Implementations;

/// <summary>
/// Provides order management services including creation, retrieval, and status updates
/// </summary>
public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    private readonly IOrderRepository _repository;
    private readonly Core.Services.OrderService _orderService;
    private readonly IOrderFactory _orderFactory;
    private readonly ILogger<OrderService> _logger;
    private readonly IMapper _mapper;

    public OrderService(
        AppDbContext context,
        IOrderRepository repository,
        Core.Services.OrderService orderService,
        ILogger<OrderService> logger,
        IMapper mapper)
    {
        _context = context;
        _repository = repository;
        _orderService = orderService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all orders");
        return await _repository.GetAllAsync();
    }

    public async Task<CreateOrderResponseDto> CreateAsync(CreateOrderDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        _logger.LogInformation("Creating new order for customer {CustomerName} with {ItemCount} items",
            dto.NamaPemesan, dto.Items?.Count ?? 0);

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var order = await _orderFactory.CreateOrderAsync(dto.NamaPemesan, dto.Items ?? new List<OrderItemDto>());
            {
                await _repository.AddAsync(order);
                await _repository.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation("Order created successfully by {CustomerName} with ID {OrderID}",
                order.NamaPemesan,order.Id);

                return _mapper.Map<CreateOrderResponseDto>(order);
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order for customer {CustomerName}", dto.NamaPemesan);
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Order?> UpdateStatusAsync(int id, UpdateStatusDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        _logger.LogInformation("Updating status for order {OrderId} with trigger {Trigger}", id, dto.Trigger);

        var order = await _repository.GetByIdAsync(id);

        if (order == null)
        {
            _logger.LogWarning("Order with ID {OrderId} not found", id);
            return null;
        }

        if (!_orderService.IsValidTransition(order.StatusSekarang, dto.Trigger))
        {
            _logger.LogWarning("Invalid status transition from {CurrentStatus} with trigger {Trigger}",
                order.StatusSekarang, dto.Trigger);

            throw new InvalidOperationException("Transisi status tidak valid");
        }

        order.StatusSekarang = _orderService.GetNextState(order.StatusSekarang, dto.Trigger);

        await _repository.SaveChangesAsync();

        _logger.LogInformation("Order {OrderId} status updated to {NewStatus}", id, order.StatusSekarang);

        return order;
    }
}