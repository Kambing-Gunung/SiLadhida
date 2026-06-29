using System.Collections.Generic;
using System.Threading.Tasks;
using SiLadhida.App.Core.Api;
using SiLadhida.App.Core.Models;
using SiLadhida.App.Features.Order;
using SiLadhida.App.Shared.Requests;
using SiLadhida.Core.Enums;

namespace SiLadhida.App.Services.Api;

public class OrderService
{
    private static OrderService? _instance;

    public static OrderService Instance
    {
        get
        {
            if (_instance == null)
                _instance = new OrderService();

            return _instance;
        }
    }

    private readonly ApiClient _client;

    private OrderService()
    {
        _client = ApiClient.Instance;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        var res = await _client.GetAsync<ApiResponse<List<Order>>>("api/orders");
        return res?.Data ?? new();
    }

    public async Task<Order?> GetOrderAsync(int id)
    {
        var res = await _client.GetAsync<ApiResponse<Order>>($"api/orders/{id}");
        return res?.Data;
    }

    public async Task<Order?> CreateOrderAsync(CreateOrderRequest request)
    {
        var res = await _client.PostAsync<ApiResponse<Order>>("api/orders", request);
        return res?.Data;
    }

    public async Task<Order?> AddItemAsync(int orderId, AddOrderItemRequest request)
    {
        var res = await _client.PostAsync<ApiResponse<Order>>(
            $"api/orders/{orderId}/items", request);

        return res?.Data;
    }

    public async Task<Order?> UpdateItemAsync(
        int orderId,
        int productId,
        UpdateQuantityRequest request)
    {
        var res = await _client.PatchAsync<ApiResponse<Order>>(
            $"api/orders/{orderId}/items/{productId}", request);

        return res?.Data;
    }

    public async Task<Order?> UpdateStatusAsync(int orderId, StateTrigger trigger)
    {
        var res = await _client.PatchAsync<ApiResponse<Order>>(
            $"api/orders/{orderId}/status",
            new { Trigger = trigger });

        return res?.Data;
    }

    public Task<Order?> PayAsync(int id)
        => UpdateStatusAsync(id, StateTrigger.PembayaranDikonfirmasi);

    public Task<Order?> CancelAsync(int id)
        => UpdateStatusAsync(id, StateTrigger.DibatalkanPelanggan);

    public Task<Order?> CompleteAsync(int id)
        => UpdateStatusAsync(id, StateTrigger.KueDiambilPelanggan);

    public Task RemoveItemAsync(int orderId, int productId)
        => _client.DeleteAsync($"api/orders/{orderId}/items/{productId}");

    public Task ClearItemsAsync(int orderId)
        => _client.DeleteAsync($"api/orders/{orderId}/items");
}