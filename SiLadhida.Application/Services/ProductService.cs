using SiLadhida.Core.Entities;
using SiLadhida.Core.Interfaces;
using SiLadhida.Application.Interfaces;
using SiLadhida.Core.Exceptions;

namespace SiLadhida.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product> GetByIdAsync(int productId)
    {
        var product = await GetProductOrThrow(productId);

        return product;
    }

    public async Task<Product> CreateAsync(string nama, decimal harga, int stock)
    {
        var product = Product.Create(nama, harga, stock);

        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateAsync(int productId, string nama, decimal harga, int stock)
    {
        var product = await GetProductOrThrow(productId);

        product.Rename(nama);
        product.UpdatePrice(harga);
        product.SetStock(stock);

        await _repository.SaveChangesAsync();

        return product;
    }

    public async Task DeleteAsync(int productId)
    {
        var product = await GetProductOrThrow(productId);

        await _repository.DeleteAsync(product);
        await _repository.SaveChangesAsync();
    }

    private async Task<Product> GetProductOrThrow(int id)
    {
        return await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Product tidak ditemukan");
    }
}