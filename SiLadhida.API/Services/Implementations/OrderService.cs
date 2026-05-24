using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SiLadhida.API.Data;
using SiLadhida.API.DTOs;
using SiLadhida.API.DTOs.Responses;
using SiLadhida.API.Repositories.Interfaces;
using SiLadhida.API.Services.Interfaces;
using SiLadhida.Core.Entities;
using SiLadhida.Core.Services;

namespace SiLadhida.API.Services.Implementations;

/// <summary>
/// Provides order management services including creation, retrieval, and status updates
/// </summary>
public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    private readonly IPesananRepository _repository;
    private readonly PesananService _pesananService;
    private readonly ILogger<OrderService> _logger;
    private readonly IMapper _mapper;

    public OrderService(
        AppDbContext context,
        IPesananRepository repository,
        PesananService pesananService,
        ILogger<OrderService> logger,
        IMapper mapper)
    {
        _context = context;
        _repository = repository;
        _pesananService = pesananService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<Pesanan>> GetAllAsync()
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
            var order = new Pesanan
            {
                NamaPemesan = dto.NamaPemesan,
                StatusSekarang = _pesananService.GetInitialStatus()
            };

            var items = new List<OrderItem>();

            // Validate and process order items
            if (dto.Items != null && dto.Items.Any())
            {
                foreach (var itemDto in dto.Items)
                {
                    var produk = await _context.Produk.FindAsync(itemDto.ProdukId);

                    if (produk == null)
                    {
                        _logger.LogError("Product with ID {ProductId} not found", itemDto.ProdukId);
                        throw new InvalidOperationException(
                            $"Produk ID {itemDto.ProdukId} tidak ditemukan");
                    }

                    if (itemDto.Quantity > produk.Stock)
                    {
                        _logger.LogError("Insufficient stock for product {ProductName}. Required: {Required}, Available: {Available}",
                            produk.Nama, itemDto.Quantity, produk.Stock);

                        throw new InvalidOperationException(
                            $"Stock produk {produk.Nama} tidak mencukupi");
                    }

                    var item = new OrderItem
                    {
                        ProdukId = produk.Id,
                        Quantity = itemDto.Quantity,
                        Harga = produk.Harga
                    };

                    produk.Stock -= itemDto.Quantity;
                    items.Add(item);
                }
            }

            order.Items = items;

            // Link items to order
            foreach (var item in items)
            {
                item.Pesanan = order;
            }

            order.TotalHarga = _pesananService.HitungTotal(items);

            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();
            await transaction.CommitAsync();

            _logger.LogInformation("Order created successfully by {CustomerName} with ID {OrderId}",
                order.NamaPemesan, order.Id);

            return _mapper.Map<CreateOrderResponseDto>(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order for customer {CustomerName}", dto.NamaPemesan);
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Pesanan?> UpdateStatusAsync(int id, UpdateStatusDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        _logger.LogInformation("Updating status for order {OrderId} with trigger {Trigger}", id, dto.Trigger);

        var order = await _repository.GetByIdAsync(id);

        if (order == null)
        {
            _logger.LogWarning("Order with ID {OrderId} not found", id);
            return null;
        }

        if (!_pesananService.IsValidTransition(order.StatusSekarang, dto.Trigger))
        {
            _logger.LogWarning("Invalid status transition from {CurrentStatus} with trigger {Trigger}",
                order.StatusSekarang, dto.Trigger);

            throw new InvalidOperationException("Transisi status tidak valid");
        }

        order.StatusSekarang = _pesananService.GetNextState(order.StatusSekarang, dto.Trigger);

        await _repository.SaveChangesAsync();

        _logger.LogInformation("Order {OrderId} status updated to {NewStatus}", id, order.StatusSekarang);

        return order;
    }
}