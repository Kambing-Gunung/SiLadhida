using Avalonia.Controls;
using SiLadhida.App.Models;

namespace SiLadhida.App.Views;

public partial class ProductDialog : Window
{
    public ProductFormModel Product { get; private set; }

    public ProductDialog()
    {
        InitializeComponent();

        Product = new ProductFormModel();

        DataContext = Product;
    }

    private void Save_Click(
    object? sender,
    Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (!Product.IsValid())
        {
            ErrorText.Text =
                "Semua field wajib valid";

            ErrorText.IsVisible = true;

            return;
        }

        Close(Product);
    }

    private void Cancel_Click(
        object? sender,
        Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close(null);
    }
}