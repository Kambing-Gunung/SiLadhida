using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using SiLadhida.App.Models;
using SiLadhida.App.ViewModels;
using SiLadhida.App.Components.Dialogs;
using System;

namespace SiLadhida.App.Views;

public partial class OrderView : UserControl
{
    public OrderView()
    {
        InitializeComponent();

        App.Navigation.SetTitle("Pesanan");

        this.AttachedToVisualTree += async (_, __) =>
        {
            if (DataContext is OrderViewModel vm)
            {
                await vm.LoadOrdersAsync();
            }
        };
    }

    private async void Manage_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        if (button.Tag is not Order order)
            return;

        var dialog = new OrderDialog(order);

        var host = new Window
        {
            Content = dialog,
            Width = 650,
            Height = 600,
            Title = "Kelola Pesanan",
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            WindowDecorations = WindowDecorations.None
        };

        var owner = TopLevel.GetTopLevel(this) as Window;

        if (owner == null)
            return;

        await host.ShowDialog(owner);

        if (DataContext is OrderViewModel vm)
        {
            await vm.RefreshAsync();
        }
    }
}