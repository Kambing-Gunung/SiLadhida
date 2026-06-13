using SiLadhida.API.Services.Interfaces;
using SiLadhida.Core.Entities;
using SiLadhida.Core.Interfaces;

namespace SiLadhida.API.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IProductRepository repository,
        ILogger<ProductService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Product> CreateAsync(
        string nama,
        decimal harga,
        int stock)
    {
        var product = Product.Create(
            nama,
            harga,
            stock);

        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();

        _logger.LogInformation(
            "Product created: {ProductName}",
            product.Nama);

        return product;
    }

    public async Task<Product?> UpdateAsync(
        int id,
        string nama,
        decimal harga,
        int stock)
    {
        var product =
            await _repository.GetByIdAsync(id);

        if (product is null)
            return null;

        product.Rename(nama);
        product.UpdatePrice(harga);
        product.SetStock(stock);

        await _repository.SaveChangesAsync();

        _logger.LogInformation(
            "Product updated: {ProductId}",
            product.Id);

        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product =
            await _repository.GetByIdAsync(id);

        if (product is null)
            return false;

        await _repository.DeleteAsync(product);
        await _repository.SaveChangesAsync();

        _logger.LogInformation(
            "Product deleted: {ProductId}",
            product.Id);

        return true;
    }
}