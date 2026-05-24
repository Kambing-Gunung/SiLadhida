using SiLadhida.Core.Entities;

namespace SiLadhida.API.Repositories.Interfaces;

/// <summary>
/// Data access interface for order (Pesanan) entities
/// </summary>
public interface IPesananRepository
{
    /// <summary>
    /// Retrieves all orders with their items and products
    /// </summary>
    Task<List<Pesanan>> GetAllAsync();

    /// <summary>
    /// Retrieves a specific order by its ID with related items and products
    /// </summary>
    Task<Pesanan?> GetByIdAsync(int id);

    /// <summary>
    /// Adds a new order to the database (does not save changes)
    /// </summary>
    Task AddAsync(Pesanan pesanan);

    /// <summary>
    /// Saves all pending changes to the database
    /// </summary>
    Task SaveChangesAsync();
}