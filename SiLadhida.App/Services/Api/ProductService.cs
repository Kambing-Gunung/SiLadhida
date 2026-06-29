using System.Collections.Generic;
using System.Threading.Tasks;
using SiLadhida.App.Core.Api;
using SiLadhida.App.Core.Models;
using SiLadhida.App.Features.Product;

namespace SiLadhida.App.Services.Api;

public class ProductService
{
    private static ProductService? _instance;

    public static ProductService Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ProductService();

            return _instance;
        }
    }

    private readonly ApiClient _client;

    private ProductService()
    {
        _client = ApiClient.Instance;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        var response =
            await _client.GetAsync<ApiResponse<List<Product>>>("api/products");

        return response?.Data ?? new();
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        var response =
            await _client.GetAsync<ApiResponse<Product>>($"api/products/{id}");

        return response?.Data;
    }

    public async Task<Product?> CreateProductAsync(Product product)
    {
        var response =
            await _client.PostAsync<ApiResponse<Product>>("api/products", product);

        return response?.Data;
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _client.PutAsync<object>($"api/products/{product.Id}", product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _client.DeleteAsync($"api/products/{id}");
    }
}