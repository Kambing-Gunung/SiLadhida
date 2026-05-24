using Microsoft.EntityFrameworkCore;
using SiLadhida.API.Data;
using SiLadhida.API.Repositories.Interfaces;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Repositories.Implementations;

/// <summary>
/// Provides data access for order (Pesanan) entities
/// </summary>
public class PesananRepository : IPesananRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<PesananRepository> _logger;

    public PesananRepository(
        AppDbContext context,
        ILogger<PesananRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Pesanan>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all orders from database");

        return await _context.Pesanan
            .Include(p => p.Items)
            .ThenInclude(i => i.Produk)
            .ToListAsync();
    }

    public async Task<Pesanan?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving order with ID {OrderId}", id);

        return await _context.Pesanan
            .Include(p => p.Items)
            .ThenInclude(i => i.Produk)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Pesanan pesanan)
    {
        ArgumentNullException.ThrowIfNull(pesanan);

        _logger.LogInformation("Adding new order to database");

        await _context.Pesanan.AddAsync(pesanan);
    }

    public async Task SaveChangesAsync()
    {
        _logger.LogInformation("Saving changes to database");

        await _context.SaveChangesAsync();
    }
}