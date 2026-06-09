using SiLadhida.API.DTOs;
using SiLadhida.API.DTOs.Responses;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Services.Interfaces;

/// <summary>
/// Service interface for order management operations
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Retrieves all orders
    /// </summary>
    Task<List<Order>> GetAllAsync();

    /// <summary>
    /// Creates a new order with items and calculates total price
    /// </summary>
    /// <param name="dto">The order creation request data</param>
    /// <returns>The created order response</returns>
    /// <exception cref="ArgumentNullException">Thrown when dto is null</exception>
    /// <exception cref="InvalidOperationException">Thrown when product not found or stock insufficient</exception>
    Task<CreateOrderResponseDto> CreateAsync(CreateOrderDto dto);

    /// <summary>
    /// Updates the status of an existing order
    /// </summary>
    /// <param name="id">The order ID</param>
    /// <param name="dto">The status update request data</param>
    /// <returns>The updated order, or null if not found</returns>
    /// <exception cref="ArgumentNullException">Thrown when dto is null</exception>
    /// <exception cref="InvalidOperationException">Thrown when status transition is invalid</exception>
    Task<Order?> UpdateStatusAsync(int id, UpdateStatusDto dto);
}