using SiLadhida.Core.Entities;
using SiLadhida.Core.Enums;

namespace SiLadhida.Application.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(int id);

    Task<Order> CreateAsync(string namaPemesan);

    Task<Order> AddItemAsync(int orderId, int productId, int quantity);
    Task<Order> UpdateStatusAsync(int id, StateTrigger trigger);
    Task<Order> UpdateItemQuantityAsync(int orderId, int productId, int quantity);

    Task<Order> RemoveItemAsync(int orderId, int productId);
    Task<Order> ResetItemsAsync(int orderId);

    Task<Order> PayAsync(int orderId, string method);
    Task<Order> CancelAsync(int orderId);
    Task<Order> CompleteAsync(int orderId);
}