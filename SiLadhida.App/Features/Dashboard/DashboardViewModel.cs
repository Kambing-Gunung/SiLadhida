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
        var orders = await _orderService.GetOrdersAsync();

        if (orders == null) return;

        TotalOrders = orders.Count;

        ActiveOrders = orders.Count(o =>
            o.StatusSekarang == StateOrder.MenungguPembayaran ||
            o.StatusSekarang == StateOrder.SiapDiambil);

        CompletedOrders = orders.Count(o =>
            o.StatusSekarang == StateOrder.Selesai);

        TotalRevenue = orders
            .Where(o => o.StatusSekarang == StateOrder.Selesai)
            .Sum(o => o.TotalHarga);

        RecentOrders = new ObservableCollection<OrderEntity>(
            orders
                .OrderByDescending(o => o.Id)
                .Take(5));
    }
}