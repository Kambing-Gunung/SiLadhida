using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiLadhida.App.Models;
using SiLadhida.App.Models.Requests;

namespace SiLadhida.App.Services;

public class OrderApiService : ApiService
{
    public async Task<List<Order>> GetOrdersAsync()
    {
        var response =
            await GetAsync<ApiResponse<List<Order>>>(
                "api/orders"
            );

        return response?.Data ?? new();
    }

    public async Task<Order?> GetOrderAsync(int id)
    {
        var response =
            await GetAsync<ApiResponse<Order>>(
                $"api/orders/{id}"
            );

        return response?.Data;
    }

    public async Task CreateOrderAsync(CreateOrderRequest request)
    {
        await PostAsync(
            "api/orders",
            request
        );
    }

    public async Task AddItemAsync(
        int orderId,
        AddOrderItemRequest request)
    {
        await PostAsync(
            $"api/orders/{orderId}/items",
            request
        );
    }

    public async Task RemoveItemAsync(
        int orderId,
        int productId)
    {
        await DeleteAsync(
            $"api/orders/{orderId}/items/{productId}"
        );
    }

    public async Task IncreaseItemAsync(
        int orderId,
        int productId,
        UpdateQuantityRequest request)
    {
        await PutAsync(
            $"api/orders/{orderId}/items/{productId}/increase",
            request
        );
    }

    public async Task DecreaseItemAsync(
        int orderId,
        int productId,
        UpdateQuantityRequest request)
    {
        await PutAsync(
            $"api/orders/{orderId}/items/{productId}/decrease",
            request
        );
    }

    public async Task ClearItemsAsync(int orderId)
    {
        await DeleteAsync(
            $"api/orders/{orderId}/items"
        );
    }

    public async Task PayAsync(int orderId)
    {
        await PutAsync(
            $"api/orders/{orderId}/pay"
        );
    }

    public async Task CancelAsync(int orderId)
    {
        await PutAsync(
            $"api/orders/{orderId}/cancel"
        );
    }

    public async Task CompleteAsync(int orderId)
    {
        await PutAsync(
            $"api/orders/{orderId}/complete"
        );
    }
}