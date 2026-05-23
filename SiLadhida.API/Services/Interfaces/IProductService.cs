using SiLadhida.Core.Entities;

namespace SiLadhida.API.Services.Interfaces;

/// <summary>
/// Provides product-related business logic and data access
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Retrieves all products
    /// </summary>
    Task<List<Produk>> GetAllAsync();

    /// <summary>
    /// Retrieves a product by its ID
    /// </summary>
    Task<Produk?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new product
    /// </summary>
    Task<Produk> CreateAsync(Produk produk);
}
