using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Models;
using SiLadhida.App.Models.Requests;
using SiLadhida.App.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SiLadhida.App.ViewModels;

public partial class KasirViewModel : ObservableObject
{
    private readonly OrderApiService _orderService;
    private readonly ProductApiService _productService;

    [ObservableProperty]
    private ObservableCollection<Order> orders = new();

    [ObservableProperty]
    private ObservableCollection<Product> products = new();

    [ObservableProperty]
    private Order? selectedOrder;

    [ObservableProperty]
    private Product? selectedProduct;

    [ObservableProperty]
    private OrderItem? selectedItem;

    [ObservableProperty]
    private string namaPemesan = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    public KasirViewModel()
    {
        _orderService = new();
        _productService = new();

        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        await LoadProductsAsync();
        await LoadOrdersAsync();
    }

    [RelayCommand]
    private async Task LoadProductsAsync()
    {
        try
        {
            var data =
                await _productService.GetProductsAsync();

            Products =
                new ObservableCollection<Product>(data);
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task LoadOrdersAsync()
    {
        try
        {
            IsLoading = true;

            var data =
                await _orderService.GetOrdersAsync();

            Orders =
                new ObservableCollection<Order>(data);
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
    private async Task CreateOrderAsync()
    {
        if (string.IsNullOrWhiteSpace(NamaPemesan))
        {
            App.Notification.ShowError(
                "Nama pemesan wajib diisi.");

            return;
        }

        try
        {
            await _orderService.CreateOrderAsync(
                new CreateOrderRequest
                {
                    NamaPemesan = NamaPemesan
                });

            NamaPemesan = string.Empty;

            await LoadOrdersAsync();

            App.Notification.ShowSuccess(
                "Pesanan berhasil dibuat.");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task AddItemAsync()
    {
        if (SelectedOrder is null)
        {
            App.Notification.ShowError(
                "Pilih pesanan terlebih dahulu.");

            return;
        }

        if (SelectedProduct is null)
        {
            App.Notification.ShowError(
                "Pilih produk terlebih dahulu.");

            return;
        }

        try
        {
            await _orderService.AddItemAsync(
                SelectedOrder.Id,
                new AddOrderItemRequest
                {
                    ProductId = SelectedProduct.Id,
                    Quantity = 1
                });

            await LoadOrdersAsync();

            App.Notification.ShowSuccess(
                "Produk berhasil ditambahkan.");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task IncreaseItemAsync()
    {
        if (SelectedOrder is null ||
            SelectedItem is null)
            return;

        try
        {
            await _orderService.IncreaseItemAsync(
                SelectedOrder.Id,
                SelectedItem.ProductId,
                new UpdateQuantityRequest
                {
                    Quantity = 1
                });

            await LoadOrdersAsync();
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task DecreaseItemAsync()
    {
        if (SelectedOrder is null ||
            SelectedItem is null)
            return;

        try
        {
            await _orderService.DecreaseItemAsync(
                SelectedOrder.Id,
                SelectedItem.ProductId,
                new UpdateQuantityRequest
                {
                    Quantity = 1
                });

            await LoadOrdersAsync();
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task RemoveItemAsync()
    {
        if (SelectedOrder is null ||
            SelectedItem is null)
            return;

        try
        {
            await _orderService.RemoveItemAsync(
                SelectedOrder.Id,
                SelectedItem.ProductId);

            await LoadOrdersAsync();
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task ClearItemsAsync()
    {
        if (SelectedOrder is null)
            return;

        try
        {
            await _orderService.ClearItemsAsync(
                SelectedOrder.Id);

            await LoadOrdersAsync();
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task PayAsync()
    {
        if (SelectedOrder is null)
            return;

        try
        {
            await _orderService.PayAsync(
                SelectedOrder.Id);

            await LoadOrdersAsync();

            App.Notification.ShowSuccess(
                "Pembayaran berhasil.");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task CompleteAsync()
    {
        if (SelectedOrder is null)
            return;

        try
        {
            await _orderService.CompleteAsync(
                SelectedOrder.Id);

            await LoadOrdersAsync();

            App.Notification.ShowSuccess(
                "Pesanan selesai.");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        if (SelectedOrder is null)
            return;

        try
        {
            await _orderService.CancelAsync(
                SelectedOrder.Id);

            await LoadOrdersAsync();

            App.Notification.ShowSuccess(
                "Pesanan dibatalkan.");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }
}