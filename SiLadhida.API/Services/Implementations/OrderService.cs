using Microsoft.EntityFrameworkCore;
using SiLadhida.API.Data;
using SiLadhida.API.DTOs;
using SiLadhida.API.DTOs.Responses;
using SiLadhida.API.Repositories.Interfaces;
using SiLadhida.API.Services.Interfaces;
using SiLadhida.Core.Entities;
using SiLadhida.Core.Services;

namespace SiLadhida.API.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    private readonly IPesananRepository _repository;
    private readonly PesananService _pesananService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(
        AppDbContext context,
        IPesananRepository repository,
        PesananService pesananService,
        ILogger<OrderService> logger)
    {
        _context = context;
        _repository = repository;
        _pesananService = pesananService;
        _logger = logger;
    }

    public async Task<List<Pesanan>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<CreateOrderResponseDto> CreateAsync(CreateOrderDto dto)
{
    using var transaction =
        await _context.Database.BeginTransactionAsync();

    var order = new Pesanan
    {
        NamaPemesan = dto.NamaPemesan,
        StatusSekarang = _pesananService.GetInitialStatus()
    };

    var items = new List<OrderItem>();

    foreach (var itemDto in dto.Items)
    {
        var produk =
            await _context.Produk.FindAsync(itemDto.ProdukId);

        if (produk == null)
            throw new Exception(
                $"Produk ID {itemDto.ProdukId} tidak ditemukan");

        if (itemDto.Quantity > produk.Stock)
            throw new Exception(
                $"Stock produk {produk.Nama} tidak mencukupi");

        var item = new OrderItem
        {
            ProdukId = produk.Id,
            Quantity = itemDto.Quantity,
            Harga = produk.Harga
        };

        produk.Stock -= itemDto.Quantity;

        items.Add(item);
    }

    order.Items = items;

    foreach (var item in items)
    {
        item.Pesanan = order;
    }

    order.TotalHarga =
        _pesananService.HitungTotal(items);

    await _repository.AddAsync(order);

    await _repository.SaveChangesAsync();

    await transaction.CommitAsync();

    _logger.LogInformation(
        "Pesanan berhasil dibuat oleh {NamaPemesan}",
        order.NamaPemesan
    );

    return new CreateOrderResponseDto
    {
        Id = order.Id,
        NamaPemesan = order.NamaPemesan,
        TotalHarga = order.TotalHarga,
        Status = order.StatusSekarang.ToString()
    };
}

    public async Task<Pesanan?> UpdateStatusAsync(int id, UpdateStatusDto dto)
    {
        var order = await _repository.GetByIdAsync(id);

        if (order == null)
            return null;

        if (!_pesananService.IsValidTransition(order.StatusSekarang, dto.Trigger))
            throw new Exception("Transisi status tidak valid");

        order.StatusSekarang =
            _pesananService.GetNextState(order.StatusSekarang, dto.Trigger);

        await _repository.SaveChangesAsync();

        return order;
    }
}