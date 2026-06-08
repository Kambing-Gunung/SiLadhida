using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SiLadhida.App.Models;

namespace SiLadhida.App.Services;

public class OrderApiService : ApiService
{
    public async Task<List<Order>> GetOrdersAsync()
    {
        var response = await HttpClient.GetFromJsonAsync<
            ApiResponse<List<Order>>
        >("api/orders");

        return response?.Data ?? new List<Order>();
    }

    public async Task CreateOrderAsync(Order order)
    {
        var response = await HttpClient.PostAsJsonAsync(
            "api/orders",
            order
        );
    }

    public async Task UpdateOrderAsync(Order order)
    {
        await HttpClient.PutAsJsonAsync(
            $"api/orders/{order.Id}",
            order
        );
    }

    public async Task DeleteOrderAsync(int id)
    {
        await HttpClient.DeleteAsync(
            $"api/orders/{id}"
        );
    }
}