using SiLadhida.Core.Entities;
using SiLadhida.API.DTOs;

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

    /// <summary>
    /// Updates an existing product
    /// </summary>
    Task<Produk?> UpdateAsync(int id, UpdateProductDto dto);

    /// <summary>
    /// Deletes a product by its ID
    /// </summary>
    Task<bool> DeleteAsync(int id);
}
