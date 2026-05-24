using SiLadhida.API.Data;
using SiLadhida.API.Services.Interfaces;
using SiLadhida.Core.Entities;
using Microsoft.EntityFrameworkCore;
using SiLadhida.API.DTOs;

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

    public async Task<Produk?> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _context.Produk
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            _logger.LogWarning(
                "Product with ID {ProductId} not found",
                id
            );

            return null;
        }

        product.Nama = dto.Nama;
        product.Harga = dto.Harga;
        product.Stock = dto.Stock;

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Product updated successfully: {ProductId}",
            id
        );

        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Produk
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            _logger.LogWarning(
                "Product with ID {ProductId} not found",
                id
            );

            return false;
        }

        _context.Produk.Remove(product);

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Product deleted successfully: {ProductId}",
            id
        );

        return true;
    }
}
