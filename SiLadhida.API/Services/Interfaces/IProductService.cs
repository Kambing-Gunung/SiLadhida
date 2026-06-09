using SiLadhida.API.DTOs;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);

    Task<Product> CreateAsync(CreateProductDto dto);

    Task<Product?> UpdateAsync(int id, UpdateProductDto dto);

    Task<bool> DeleteAsync(int id);
}