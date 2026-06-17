using SiLadhida.Core.Entities;

namespace SiLadhida.API.Services.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetAllAsync();

    Task<Order?> GetByIdAsync(int id);

    Task<Order> CreateAsync(string namaPemesan);

    Task<Order?> AddItemAsync(
        int orderId,
        int productId,
        int quantity);

    Task<Order?> RemoveItemAsync(
        int orderId,
        int productId);

    Task<Order?> IncreaseItemAsync(
        int orderId,
        int productId,
        int quantity);

    Task<Order?> DecreaseItemAsync(
        int orderId,
        int productId,
        int quantity);

    Task<Order?> ClearItemsAsync(
        int orderId);

    Task<Order?> PayOrderAsync(
        int orderId);

    Task<Order?> CancelOrderAsync(
        int orderId);

    Task<Order?> CompleteOrderAsync(
        int orderId);
}