using CommunityToolkit.Mvvm.ComponentModel;
using SiLadhida.App.Services.Api;
using SiLadhida.Core.Enums;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using OrderEntity = SiLadhida.App.Features.Order.Order;

namespace SiLadhida.App.Features.Dashboard;

public partial class DashboardViewModel : ObservableObject
{
    private readonly OrderService _orderService;

    [ObservableProperty]
    private int totalOrders;

    [ObservableProperty]
    private int activeOrders;

    [ObservableProperty]
    private int completedOrders;

    [ObservableProperty]
    private decimal totalRevenue;

    [ObservableProperty]
    private ObservableCollection<OrderEntity> recentOrders = new();

    public DashboardViewModel()
    {
        _orderService = App.Services.OrderService;
        _ = LoadAsync();
    }

    private async Task LoadAsync()
    {
        // 1. Ambil data mentah dari API
        var rawOrders = await _orderService.GetOrdersAsync();

        if (rawOrders == null) return;

        // 2. Filter data: Buang pesanan yang statusnya Dibatalkan (atau Dihapus)
        // Gunakan .ToList() agar hasil filter ini tersimpan rapi sebagai List
        var validOrders = rawOrders.Where(o => o.StatusSekarang != StateOrder.Dibatalkan).ToList();

        // 3. Gunakan 'validOrders' untuk SEMUA perhitungan di bawah ini, bukan data mentahnya

        TotalOrders = validOrders.Count;

        ActiveOrders = validOrders.Count(o =>
            o.StatusSekarang == StateOrder.MenungguPembayaran ||
            o.StatusSekarang == StateOrder.SiapDiambil);

        CompletedOrders = validOrders.Count(o =>
            o.StatusSekarang == StateOrder.Selesai);

        TotalRevenue = validOrders
            .Where(o => o.StatusSekarang == StateOrder.Selesai)
            .Sum(o => o.TotalHarga);

        RecentOrders = new ObservableCollection<OrderEntity>(
            validOrders
                .OrderByDescending(o => o.Id)
                .Take(5));
    }
}