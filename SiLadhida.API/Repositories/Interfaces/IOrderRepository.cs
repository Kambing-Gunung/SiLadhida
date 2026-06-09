using SiLadhida.Core.Entities;

namespace SiLadhida.API.Repositories.Interfaces;

/// <summary>
/// Data access interface for order (Pesanan) entities
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Retrieves all orders with their items and products
    /// </summary>
    Task<List<Order>> GetAllAsync();

    /// <summary>
    /// Retrieves a specific order by its ID with related items and products
    /// </summary>
    Task<Order?> GetByIdAsync(int id);

    /// <summary>
    /// Adds a new order to the database (does not save changes)
    /// </summary>
    Task AddAsync(Order order);

    /// <summary>
    /// Saves all pending changes to the database
    /// </summary>
    Task SaveChangesAsync();
}