using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SiLadhida.App.Models;
using SiLadhida.App.Models.Requests;

namespace SiLadhida.App.Services;

public class OrderApiService : ApiService
{
    public async Task<List<Order>> GetOrdersAsync()
    {
        var response =
            await HttpClient.GetFromJsonAsync<
                ApiResponse<List<Order>>
            >("api/orders");

        return response?.Data ?? new();
    }

    public async Task<Order?> GetOrderAsync(int id)
    {
        var response =
            await HttpClient.GetFromJsonAsync<
                ApiResponse<Order>
            >($"api/orders/{id}");

        return response?.Data;
    }

    public async Task CreateOrderAsync(
        CreateOrderRequest request)
    {
        await HttpClient.PostAsJsonAsync(
            "api/orders",
            request);
    }

    public async Task AddItemAsync(
        int orderId,
        AddOrderItemRequest request)
    {
        await HttpClient.PostAsJsonAsync(
            $"api/orders/{orderId}/items",
            request);
    }

    public async Task RemoveItemAsync(
        int orderId,
        int productId)
    {
        await HttpClient.DeleteAsync(
            $"api/orders/{orderId}/items/{productId}");
    }

    public async Task IncreaseItemAsync(
        int orderId,
        int productId,
        UpdateQuantityRequest request)
    {
        await HttpClient.PutAsJsonAsync(
            $"api/orders/{orderId}/items/{productId}/increase",
            request);
    }

    public async Task DecreaseItemAsync(
        int orderId,
        int productId,
        UpdateQuantityRequest request)
    {
        await HttpClient.PutAsJsonAsync(
            $"api/orders/{orderId}/items/{productId}/decrease",
            request);
    }

    public async Task ClearItemsAsync(
        int orderId)
    {
        await HttpClient.DeleteAsync(
            $"api/orders/{orderId}/items");
    }

    public async Task PayAsync(
        int orderId)
    {
        await HttpClient.PutAsync(
            $"api/orders/{orderId}/pay",
            null);
    }

    public async Task CancelAsync(
        int orderId)
    {
        await HttpClient.PutAsync(
            $"api/orders/{orderId}/cancel",
            null);
    }

    public async Task CompleteAsync(
        int orderId)
    {
        await HttpClient.PutAsync(
            $"api/orders/{orderId}/complete",
            null);
    }
}