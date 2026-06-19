using System.Collections.Generic;
using System.Threading.Tasks;
using SiLadhida.App.Models;

namespace SiLadhida.App.Services;

public class ProductApiService : ApiService
{
    public async Task<List<Product>> GetProductsAsync()
    {
        var response =
            await GetAsync<ApiResponse<List<Product>>>(
                "api/products"
            );

        return response?.Data ?? new();
    }

    public async Task CreateProductAsync(Product product)
    {
        await PostAsync(
            "api/products",
            product
        );
    }

    public async Task UpdateProductAsync(Product product)
    {
        await PutAsync(
            $"api/products/{product.Id}",
            product
        );
    }

    public async Task DeleteProductAsync(int id)
    {
        await DeleteAsync(
            $"api/products/{id}"
        );
    }
}