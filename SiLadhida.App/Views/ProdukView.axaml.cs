using Avalonia.Controls;
using SiLadhida.App.Models;
using SiLadhida.App.ViewModels;
using SiLadhida.App.Views;

namespace SiLadhida.App.Views;

public partial class ProdukView : UserControl
{
    public ProdukView()
    {
        InitializeComponent();
    }

    private async void AddProduct_Click(
        object? sender,
        Avalonia.Interactivity.RoutedEventArgs e)
    {
        var dialog = new ProductDialog();

        var mainWindow =
            TopLevel.GetTopLevel(this) as Window;

        if (mainWindow == null)
            return;

        var result =
            await dialog.ShowDialog<ProductFormModel?>(mainWindow);

        if (result == null)
            return;

        if (DataContext is ProdukViewModel vm)
        {
            var product = new Product
            {
                Nama = result.Nama,
                Harga = result.Harga,
                Stock = result.Stock
            };

            await vm.CreateProductAsync(product);
        }
    }
}