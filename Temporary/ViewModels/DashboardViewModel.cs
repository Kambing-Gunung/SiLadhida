using CommunityToolkit.Mvvm.ComponentModel;
using SiLadhida.App.Services;
using SiLadhida.App.Models;
using System.Linq;
using System.Threading.Tasks;
using System;
using SiLadhida.Core.Enums;

namespace SiLadhida.App.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    private readonly ProductApiService _productService;
    private readonly OrderApiService _orderService;

    [ObservableProperty]
    private int totalProduk;

    [ObservableProperty]
    private int totalOrder;

    [ObservableProperty]
    private int totalTransaksi;

    [ObservableProperty]
    private bool isLoading;

    public DashboardViewModel()
    {
        _productService = new ProductApiService();
        _orderService = new OrderApiService();
    }

    public DashboardViewModel(ProductApiService productService, OrderApiService orderService)
    {
        _productService = productService;
        _orderService = orderService;
    }

    public async Task LoadAsync()
    {
        try
        {
            IsLoading = true;

            var products = await _productService.GetProductsAsync();
            var orders = await _orderService.GetOrdersAsync();

            TotalProduk = products.Count;

            TotalOrder = orders.Count(o =>
                o.StatusSekarang == StateOrder.MenungguPembayaran ||
                o.StatusSekarang == StateOrder.SiapDiambil);

            TotalTransaksi = orders.Count(o =>
                o.StatusSekarang == StateOrder.Selesai);
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
}