using Avalonia.Controls;
using SiLadhida.App.Features.Auth;
using SiLadhida.App.Features.Dashboard;
using SiLadhida.App.Features.Product;
using SiLadhida.App.Features.Order;
using SiLadhida.App.Features.Transaction;
using SiLadhida.App.Services.App;

namespace SiLadhida.App.Components.Shell;

public partial class Sidebar : UserControl
{
    private readonly NavigationService _navigation;

    public Sidebar()
    {
        InitializeComponent();

        _navigation = App.Services.Navigation;

        SetActiveButton(DashboardButton);
    }

    private Button? _activeButton;

    private void Dashboard_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(DashboardButton);
        _navigation.NavigateContent(new DashboardViewModel(), "Dashboard");
    }

    private void Product_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(ProdukButton);
        _navigation.NavigateContent(new ProductViewModel(), "Produk");
    }

    private void Order_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(PesananButton);
        _navigation.NavigateContent(new OrderViewModel(), "Pesanan");
    }

    private void Transaction_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(TransaksiButton);
        _navigation.NavigateContent(new TransactionViewModel(), "Transaksi");
    }

    private void Logout_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(LogoutButton);
        App.Services.AuthSession.Clear();
        _navigation.NavigateToLogin();
    }

    private void SetActiveButton(Button button)
    {
        _activeButton?.Classes.Remove("active");

        button.Classes.Add("active");

        _activeButton = button;
    }
}