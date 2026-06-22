using System;
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
            Harga = product.Harga,
            Stock = product.Stock,
            Title = "Edit Produk"
        };
    }

    private async void Save_Click(object? sender, RoutedEventArgs e)
    {
        var model = DataContext as ProductFormModel;

        if (model == null || !model.IsValid())
            return;

        var dialog = this.FindAncestorOfType<BaseDialog>();

        if (dialog != null)
        {
            await dialog.CloseWithAnimation(model);
        }
    }

    private async void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = this.FindAncestorOfType<BaseDialog>();

        if (dialog != null)
        {
            await dialog.CloseWithAnimation(null);
        }
    }
}