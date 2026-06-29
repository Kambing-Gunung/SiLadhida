using System.Globalization;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using SiLadhida.App.Components.Dialogs;

namespace SiLadhida.App.Features.Product;

public partial class ProductDialog : UserControl
{
    public ProductDialog()
    {
        InitializeComponent();
        DataContext = new ProductFormModel();
    }

    public ProductDialog(Product product)
    {
        InitializeComponent();

        DataContext = new ProductFormModel
        {
            Nama = product.Nama,
            HargaText = product.Harga.ToString("0.##", CultureInfo.InvariantCulture),
            StockText = product.Stock.ToString(CultureInfo.InvariantCulture),
            Title = "Edit Produk",
            IsEdit = true
        };
    }

    private async void Save_Click(object? sender, RoutedEventArgs e)
    {
        var model = DataContext as ProductFormModel;
        if (model == null || !model.IsValid())
            return;

        var dialog = this.FindAncestorOfType<BaseDialog>();
        if (dialog != null)
            await dialog.CloseWithAnimation(model);
    }

    private async void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = this.FindAncestorOfType<BaseDialog>();
        if (dialog != null)
            await dialog.CloseWithAnimation(null);
    }
}
