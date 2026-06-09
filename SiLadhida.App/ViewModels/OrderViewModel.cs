using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Models;
using SiLadhida.App.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SiLadhida.App.ViewModels;

public partial class OrderViewModel : ObservableObject
{
    private readonly OrderApiService _service;

    private ObservableCollection<Order> _orders = new();
    public ObservableCollection<Order> Orders
    {
        get => _orders;
        set => SetProperty(ref _orders, value);
    }

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public OrderViewModel()
    {
        _service = new OrderApiService();

        _ = LoadOrdersAsync();
    }

    [RelayCommand]
    private async Task LoadOrdersAsync()
    {
        try
        {
            IsLoading = true;

            var data = await _service.GetOrdersAsync();

            Orders = new ObservableCollection<Order>(data ?? []);
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task CreateOrderAsync(Order order)
    {
        await _service.CreateOrderAsync(order);

        await LoadOrdersAsync();

        App.Notification.ShowSuccess("Pesanan berhasil ditambahkan");
    }

    [RelayCommand]
    public async Task UpdateOrderAsync(Order order)
    {
        if (order == null || order.Id <= 0)
        {
            App.Notification.ShowError("Pesanan tidak valid");
            return;
        }

        await _service.UpdateOrderAsync(order);

        await LoadOrdersAsync();

        App.Notification.ShowSuccess("Pesanan berhasil diperbarui");
    }

    [RelayCommand]
    public async Task DeleteOrderAsync(Order order)
    {
        if (order == null || order.Id <= 0)
        {
            App.Notification.ShowError("Pesanan tidak valid");
            return;
        }

        await _service.DeleteOrderAsync(order.Id);

        await LoadOrdersAsync();

        App.Notification.ShowSuccess("Pesanan berhasil dihapus");
    }
}
