using System.Collections.Generic;
using System.Threading.Tasks;
using SiLadhida.App.Models;
using SiLadhida.App.Services.Core;

namespace SiLadhida.App.Services.Api;

public class ProductService
{
    private readonly ApiClient _client;

    public ProductService(ApiClient client)
    {
        _client = client;
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

    public async Task CreateProductAsync(Product product)
    {
        await _client.PostAsync<object>("api/products", product);
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