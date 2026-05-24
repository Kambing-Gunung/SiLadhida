using SiLadhida.API.Data;
using SiLadhida.API.Services.Interfaces;
using SiLadhida.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace SiLadhida.API.Services.Implementations;

/// <summary>
/// Provides product management services
/// </summary>
public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        AppDbContext context,
        ILogger<ProductService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Produk>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all products");
        return await _context.Produk.ToListAsync();
    }

    public async Task<Produk?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving product with ID {ProductId}", id);
        return await _context.Produk.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Produk> CreateAsync(Produk produk)
    {
        ArgumentNullException.ThrowIfNull(produk);

        _logger.LogInformation("Creating new product: {ProductName}", produk.Nama);

        _context.Produk.Add(produk);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Product created successfully with ID {ProductId}", produk.Id);

        return produk;
    }
}
