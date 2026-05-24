using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using SiLadhida.App.Models;

namespace SiLadhida.App.Services;

public class ProductApiService : ApiService
{
    public async Task<List<Product>> GetProductsAsync()
    {
        var response = await HttpClient.GetFromJsonAsync<
            ApiResponse<List<Product>>
        >("api/products");

        return response?.Data ?? new List<Product>();
    }

    public async Task CreateProductAsync(Product product)
    {
        await HttpClient.PostAsJsonAsync(
            "api/products",
            product
        );
    }

    public async Task UpdateProductAsync(Product product)
    {
        await HttpClient.PutAsJsonAsync(
            $"api/products/{product.Id}",
            product
        );
    }

    public async Task DeleteProductAsync(int id)
    {
        await HttpClient.DeleteAsync(
            $"api/products/{id}"
        );
    }
}