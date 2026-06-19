using Avalonia.Controls;
using SiLadhida.App.Views;
using Avalonia.Media;

namespace SiLadhida.App.Components;

public partial class Sidebar : UserControl
{
    public Sidebar()
    {
        InitializeComponent();

        SetActiveButton(DashboardButton);
    }

    private Button? _activeButton;

    private void Dashboard_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(DashboardButton);
        App.Navigation.Navigate(new DashboardView(), "Dashboard");
    }

    private void Product_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(ProdukButton);
        App.Navigation.Navigate(new ProductView(), "Produk");
    }

    private void Order_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(PesananButton);
        App.Navigation.Navigate(new OrderView(), "Pesanan");
    }

    private void Transaction_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(TransaksiButton);
        App.Navigation.Navigate(new TransactionView(), "Transaksi");
    }

    private void Logout_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(LogoutButton);
        App.Navigation.Navigate(new LoginView(), "Dashboard");
    }

    private void SetActiveButton(Button button)
    {
        _activeButton?.Classes.Remove("active");

        button.Classes.Add("active");

        _activeButton = button;
    }
}