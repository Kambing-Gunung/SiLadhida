using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Models;
using SiLadhida.App.Models.Requests;
using SiLadhida.App.Services;
using SiLadhida.Core.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SiLadhida.App.ViewModels;

public partial class OrderViewModel : ObservableObject
{
    private readonly OrderApiService _service;

    [ObservableProperty]
    private ObservableCollection<Order> orders = new();

    [ObservableProperty]
    private Order? selectedOrder;

    [ObservableProperty]
    private string namaPemesan = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    public OrderViewModel()
    {
        _service = new();

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
    public async Task RefreshAsync()
    {
        await LoadOrdersAsync();
    }

    [RelayCommand]
    private async Task CreateOrderAsync()
    {
        if (string.IsNullOrWhiteSpace(NamaPemesan))
        {
            App.Notification.ShowError("Nama pemesan wajib diisi.");

            return;
        }

        try
        {
            var request = new CreateOrderRequest
            {
                NamaPemesan = NamaPemesan
            };

            await _service.CreateOrderAsync(request);

            NamaPemesan = string.Empty;

            await LoadOrdersAsync();
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task PayOrderAsync(Order order)
    {
        if (order == null)
            return;

        try
        {
            await _service.PayAsync(order.Id);

            await LoadOrdersAsync();

            App.Notification.ShowSuccess("Pembayaran berhasil");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task CancelOrderAsync(Order order)
    {
        if (order == null)
            return;

        try
        {
            await _service.CancelAsync(order.Id);

            await LoadOrdersAsync();

            App.Notification.ShowSuccess("Order dibatalkan");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task CompleteOrderAsync(Order order)
    {
        if (order == null)
            return;

        try
        {
            await _service.CompleteAsync(order.Id);

            await LoadOrdersAsync();

            App.Notification.ShowSuccess("Pesanan selesai");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    public ObservableCollection<Order> ActiveOrders => new(Orders.Where(o =>
        o.StatusSekarang == StateOrder.MenungguPembayaran ||
        o.StatusSekarang == StateOrder.SiapDiambil));
}