using SiLadhida.Core.Entities;

namespace SiLadhida.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<List<Product>> GetByIdsAsync(
            IEnumerable<int> ids);

        Task AddAsync(Product product);

        Task DeleteAsync(Product product);

        Task SaveChangesAsync();
    }
}