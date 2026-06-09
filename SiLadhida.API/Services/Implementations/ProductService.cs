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

    public async Task<List<Product>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all products");
        return await _context.Product.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Retrieving product with ID {ProductId}", id);
        return await _context.Product.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> CreateAsync(CreateProductDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var product = new Product
        {
            Nama = dto.Nama,
            Harga = dto.Harga,
            Stock = dto.Stock
        };

        _logger.LogInformation("Creating product: {Name}", dto.Nama);

        _context.Product.Add(product);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Product created with ID {Id}", product.Id);

        return product;
    }

    public async Task<Product?> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product = await _context.Product
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
        var product = await _context.Product
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            _logger.LogWarning(
                "Product with ID {ProductId} not found",
                id
            );

            return false;
        }

        _context.Product.Remove(product);

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Product deleted successfully: {ProductId}",
            id
        );

        return true;
    }
}
