using System;
using Avalonia.Controls;
using SiLadhida.App.Models;
using SiLadhida.App.ViewModels;
using SiLadhida.App.Views;
using SiLadhida.App.Components;

namespace SiLadhida.App.Views;

public partial class OrderView : UserControl
{
    public OrderView()
    {
        InitializeComponent();
    }

    private async void AddOrder_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var content = new SiLadhida.App.Components.Dialogs.OrderDialog();

        var host = new Window
        {
            Content = content,
            Width = 400,
            Height = 450,
            Title = "Tambah Order",
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            WindowDecorations = WindowDecorations.None
        };

        var mainWindow = TopLevel.GetTopLevel(this) as Window;

        if (mainWindow == null)
            return;

        var result = await host.ShowDialog<OrderFormModel?>(mainWindow);

        if (result == null)
            return;

        if (DataContext is OrderViewModel vm)
        {
            var order = new Order
            {
                NamaPemesan = result.NamaPemesan,
                StatusSekarang = result.StatusSekarang,
                TotalHarga = result.TotalHarga,
                Items = result.Items,
            };

            await vm.CreateOrderAsync(order);
        }
    }

    private async void Delete_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        if (button.Tag is not Order order)
            return;

        var dialog = new ConfirmDialog(
            $"Hapus order '{order.NamaPemesan}' ?");

        var mainWindow =
            TopLevel.GetTopLevel(this) as Window;

        if (mainWindow == null)
            return;

        var confirmed =
            await dialog.ShowDialog<bool>(mainWindow);

        if (!confirmed)
            return;

        if (DataContext is OrderViewModel vm)
        {
            await vm.DeleteOrderAsync(order);
        }
    }

    private async void Edit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is not Button button)
            return;

        if (button.Tag is not Order order)
            return;

        var content = new SiLadhida.App.Components.Dialogs.OrderDialog(order);

        var host = new Window
        {
            Content = content,
            Width = 400,
            Height = 350,
            Title = "Edit Order",
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            WindowDecorations = WindowDecorations.None
        };

        var mainWindow = TopLevel.GetTopLevel(this) as Window;

        if (mainWindow == null)
            return;

        var result = await host.ShowDialog<OrderFormModel?>(mainWindow);

        if (result == null)
            return;

        if (DataContext is OrderViewModel vm)
        {
            order.NamaPemesan = result.NamaPemesan;
            order.StatusSekarang = result.StatusSekarang;
            order.TotalHarga = result.TotalHarga;
            order.Items = result.Items;

            await vm.UpdateOrderAsync(order);
        }
    }
}
