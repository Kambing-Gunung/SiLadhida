using SiLadhida.Core.Entities;
using SiLadhida.Core.Enums;

namespace SiLadhida.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(int id);

        Task<Order> CreateAsync(string namaPemesan);

        Task<Order?> UpdateStatusAsync(
            int id,
            StateTrigger trigger);

        Task<Order?> PayOrderAsync(int orderId);
    }
}