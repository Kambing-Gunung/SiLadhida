using Avalonia.Controls;
using SiLadhida.App.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace SiLadhida.App.Components.Dialogs;

public partial class OrderItemDialog : UserControl
{
    // 🔥 HARUS PROPERTY (bukan field)
    public ObservableCollection<Product> Products { get; set; } = new();

    public OrderItemDialog()
    {
        InitializeComponent();
        DataContext = this;
    }

    public OrderItemDialog(ObservableCollection<Product> products)
        : this()
    {
        Products = products;

        // 🔥 penting biar UI update
        DataContext = null;
        DataContext = this;
    }

    private void Submit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var selected = Products
            .Where(p => p.IsSelected)
            .ToList();

        var window = TopLevel.GetTopLevel(this) as Window;

        window?.Close(selected);
    }

    private void Cancel_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as Window;
        window?.Close(null);
    }
}