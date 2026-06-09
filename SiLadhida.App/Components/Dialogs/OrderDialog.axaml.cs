using Avalonia.Controls;
using SiLadhida.App.Models;

namespace SiLadhida.App.Components.Dialogs;

public partial class OrderDialog : UserControl
{
    public OrderFormModel Order { get; private set; }

    public OrderDialog()
    {
        InitializeComponent();

        Order = new OrderFormModel();

        DataContext = Order;
    }

    public OrderDialog(Order order)
    {
        InitializeComponent();

        Order = new OrderFormModel
        {
            NamaPemesan = order.NamaPemesan,
            StatusSekarang = order.StatusSekarang,
            TotalHarga = order.TotalHarga,
            Items = order.Items
        };

        DataContext = Order;
    }

    private void Save_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (!Order.IsValid())
        {
            ErrorText.Text =
                "Semua field wajib valid";

            ErrorText.IsVisible = true;

            return;
        }

        var window = TopLevel.GetTopLevel(this) as Window;
        window?.Close(Order);
    }

    private void Cancel_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as Window;
        window?.Close(null);
    }
}