using Microsoft.EntityFrameworkCore;
using SiLadhida.API.Data;
using SiLadhida.API.Repositories.Interfaces;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Repositories.Implementations;

/// <summary>
/// Provides data access for order (Pesanan) entities
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<OrderRepository> _logger;

    public OrderRepository(
        AppDbContext context,
        ILogger<OrderRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all orders from database");

        return await _context.Order
            .Include(p => p.Items)
            .ThenInclude(i => i.Product)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving order with ID {OrderId}", id);

        return await _context.Order
            .Include(p => p.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);

        _logger.LogInformation("Adding new order to database");

        await _context.Order.AddAsync(order);
    }

    public async Task SaveChangesAsync()
    {
        _logger.LogInformation("Saving changes to database");

        await _context.SaveChangesAsync();
    }
}