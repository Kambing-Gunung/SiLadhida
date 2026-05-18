using Avalonia.Controls;
using Avalonia.Interactivity;
using SiLadhida.App.Views;

namespace SiLadhida.App.Components;

public partial class Sidebar : UserControl
{
    public Sidebar()
    {
        InitializeComponent();
    }

    private void Dashboard_Click(object? sender, RoutedEventArgs e)
    {
        App.Navigation.Navigate(new DashboardView());
    }

    private void Kasir_Click(object? sender, RoutedEventArgs e)
    {
        App.Navigation.Navigate(new KasirView());
    }

    private void Produk_Click(object? sender, RoutedEventArgs e)
    {
        App.Navigation.Navigate(new ProdukView());
    }

    private void Pesanan_Click(object? sender, RoutedEventArgs e)
    {
        App.Navigation.Navigate(new PesananView());
    }
}