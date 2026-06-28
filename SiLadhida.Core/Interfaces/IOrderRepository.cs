using SiLadhida.Core.Entities;

namespace SiLadhida.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(int id);

        Task AddAsync(Order order);

        Task SaveChangesAsync();

        void Delete(Order order);
    }
}