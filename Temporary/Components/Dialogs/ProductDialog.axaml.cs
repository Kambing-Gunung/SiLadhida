using Avalonia.Controls;
using Avalonia.Interactivity;
using SiLadhida.App.Models;

namespace SiLadhida.App.Components.Dialogs;

public partial class ProductDialog : UserControl
{
    public ProductFormModel Product { get; private set; }

    public ProductDialog()
    {
        InitializeComponent();

        Product = new ProductFormModel();

        DataContext = Product;
    }

    public ProductDialog(Product product)
    {
        InitializeComponent();

        Product = new ProductFormModel
        {
            Nama = product.Nama,
            Harga = product.Harga,
            Stock = product.Stock,
            Title = "Edit Produk"
        };

        DataContext = Product;
    }

    private void Save_Click(object? sender, RoutedEventArgs e)
    {
        if (!Product.IsValid())
        {
            Product.ErrorMessage = "Semua field wajib valid";
            return;
        }

        var window = TopLevel.GetTopLevel(this) as Window;
        window?.Close(Product);
    }

    private void Cancel_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as Window;
        window?.Close(null);
    }
}