using Avalonia.Controls;
using SiLadhida.App.Models;
using SiLadhida.App.ViewModels;
using SiLadhida.App.Components;

namespace SiLadhida.App.Views;

public partial class ProdukView : UserControl
{
    public ProdukView()
    {
        InitializeComponent();
    }

    private async void AddProduct_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var content = new SiLadhida.App.Components.Dialogs.ProductDialog();
        var host = new Window
        {
            Content = content,
            Width = 400,
            Height = 400,
            Title = "Tambah Produk",
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            WindowDecorations = Avalonia.Controls.WindowDecorations.None
        };

        var mainWindow = TopLevel.GetTopLevel(this) as Window;
        if (mainWindow == null) return;

        var result = await host.ShowDialog<ProductFormModel?>(mainWindow);

        if (result != null && DataContext is ProdukViewModel viewmodel)
        {
            var product = new Product { 
                Nama = result.Nama, 
                Harga = result.Harga, 
                Stock = result.Stock };
            await viewmodel.CreateProductAsync(product);
        }
    }

    private async void Edit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Product product)
        {
            var content = new SiLadhida.App.Components.Dialogs.ProductDialog(product);
            var host = new Window
            {
                Content = content,
                Width = 400,
                Height = 400,
                Title = "Edit Produk",
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                WindowDecorations = Avalonia.Controls.WindowDecorations.None,
            };

            var mainWindow = TopLevel.GetTopLevel(this) as Window;
            if (mainWindow == null) return;

            var result = await host.ShowDialog<ProductFormModel?>(mainWindow);

            if (result != null && DataContext is ProdukViewModel viewmodel)
            {
                product.Nama = result.Nama;
                product.Harga = result.Harga;
                product.Stock = result.Stock;
                await viewmodel.UpdateProductAsync(product);
            }
        }
    }

    private async void Delete_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Product product)
        {
            var dialog = new ConfirmDialog($"Hapus produk '{product.Nama}' ?");
            var mainWindow = TopLevel.GetTopLevel(this) as Window;
            if (mainWindow == null) return;

            var confirmed = await dialog.ShowDialog<bool>(mainWindow);

            if (confirmed && DataContext is ProdukViewModel viewmodel)
            {
                await viewmodel.DeleteProductAsync(product);
            }
        }
    }
}