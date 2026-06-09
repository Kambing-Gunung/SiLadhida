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
        App.Navigation.Navigate(new DashboardView());
    }

    private void Produk_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(ProdukButton);
        App.Navigation.Navigate(new ProdukView());
    }

    private void Pesanan_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(PesananButton);
        App.Navigation.Navigate(new OrderView());
    }

    private void Kasir_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(KasirButton);
        App.Navigation.Navigate(new KasirView());
    }

    private void Logout_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        SetActiveButton(LogoutButton);
        App.Navigation.Navigate(new LoginView());
    }

    private void SetActiveButton(Button button)
    {
        if (_activeButton != null)
        {
            _activeButton.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x4E, 0x34, 0x2E));
        }

        button.Background = new SolidColorBrush(Color.FromArgb(0x25, 0x00, 0x00, 0x00));
        _activeButton = button;
    }
}