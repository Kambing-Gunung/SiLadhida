using System;
using Avalonia.Controls;
using SiLadhida.App.Models;
using SiLadhida.App.ViewModels;
using SiLadhida.App.Views;
using SiLadhida.App.Components;
using Avalonia.Interactivity;
using SiLadhida.App.Components.Dialogs;

namespace SiLadhida.App.Views;

public partial class OrderView : UserControl
{
    public OrderView()
    {
        InitializeComponent();
    }

    private async void Manage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        if (button.Tag is not Order order)
            return;

        var dialog = new OrderDialog(order);

        if (DataContext is OrderViewModel vm)
        {
            await vm.RefreshAsync();
        }
        
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

        // optional (safety)
        if (DataContext is OrderViewModel vm2)
        {
            await vm2.RefreshAsync();
        }
    }
}
