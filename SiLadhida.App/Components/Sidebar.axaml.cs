using Avalonia.Controls;
using SiLadhida.App.Views;

namespace SiLadhida.App.Components;

public partial class Sidebar : UserControl
{
    public Sidebar()
    {
        InitializeComponent();
    }

    private void Dashboard_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        App.Navigation.Navigate(new DashboardView());
    }

    private void Produk_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        App.Navigation.Navigate(new ProdukView());
    }

    private void Pesanan_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        App.Navigation.Navigate(new PesananView());
    }

    private void Kasir_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        App.Navigation.Navigate(new KasirView());
    }

    private void Logout_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        App.Navigation.Navigate(new LoginView());
    }
}