using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SiLadhida.App.Services;

namespace SiLadhida.App;

public partial class MainWindow : Window
{
    private readonly ApiService _apiService;

    public MainWindow()
    {
        InitializeComponent();

        _apiService = new ApiService();

        var btn = this.FindControl<Button>("BtnLoad");
        btn.Click += LoadOrders;
    }

    private async void LoadOrders(object? sender, RoutedEventArgs e)
    {
        var orders = await _apiService.GetOrders();

        var listBox = this.FindControl<ListBox>("OrderList");

        listBox.ItemsSource = orders.Select(o =>
            $"#{o.Id} | {o.NamaPemesan} | {o.StatusSekarang} | Rp{o.TotalHarga}");
    }
}