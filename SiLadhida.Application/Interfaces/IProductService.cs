using SiLadhida.Core.Entities;

namespace SiLadhida.Application.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);

    Task<Product> CreateAsync(string nama, decimal harga, int stock);
    Task<Product> UpdateAsync(int id, string nama, decimal harga, int stock);

    Task DeleteAsync(int id);
}