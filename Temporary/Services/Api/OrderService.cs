using System.Collections.Generic;
using System.Threading.Tasks;
using SiLadhida.App.Models;
using SiLadhida.App.Models.Requests;
using SiLadhida.App.Services.Core;

namespace SiLadhida.App.Services.Api;

public class OrderService
{
    private readonly ApiClient _client;

    public OrderService(ApiClient client)
    {
        _client = client;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        var response =
            await _client.GetAsync<ApiResponse<List<Order>>>("api/orders");

        return response?.Data ?? new();
    }

    public async Task<Order?> GetOrderAsync(int id)
    {
        var response =
            await _client.GetAsync<ApiResponse<Order>>($"api/orders/{id}");

        return response?.Data;
    }

    public async Task<Order?> CreateOrderAsync(CreateOrderRequest request)
    {
        var response =
            await _client.PostAsync<ApiResponse<Order>>("api/orders", request);

        return response?.Data;
    }

    public async Task<Order?> AddItemAsync(int orderId, AddOrderItemRequest request)
    {
        var response =
            await _client.PostAsync<ApiResponse<Order>>(
                $"api/orders/{orderId}/items",
                request);

        return response?.Data;
    }

    public async Task<Order?> UpdateItemAsync(
        int orderId,
        int productId,
        UpdateQuantityRequest request)
    {
        var response =
            await _client.PutAsync<ApiResponse<Order>>(
                $"api/orders/{orderId}/items/{productId}",
                request);

        return response?.Data;
    }

    public async Task<Order?> PayAsync(int orderId)
    {
        var response =
            await _client.PutAsync<ApiResponse<Order>>($"api/orders/{orderId}/pay");

        return response?.Data;
    }

    public async Task<Order?> CancelAsync(int orderId)
    {
        var response =
            await _client.PutAsync<ApiResponse<Order>>($"api/orders/{orderId}/cancel");

        return response?.Data;
    }

    public async Task<Order?> CompleteAsync(int orderId)
    {
        var response =
            await _client.PutAsync<ApiResponse<Order>>($"api/orders/{orderId}/complete");

        return response?.Data;
    }

    public async Task RemoveItemAsync(int orderId, int productId)
    {
        await _client.DeleteAsync($"api/orders/{orderId}/items/{productId}");
    }

    public async Task ClearItemsAsync(int orderId)
    {
        await _client.DeleteAsync($"api/orders/{orderId}/items");
    }
}