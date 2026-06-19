using Avalonia.Controls;
using SiLadhida.App.Models;

namespace SiLadhida.App.Components.Dialogs;

public partial class TransactionDialog : UserControl
{
    public TransactionDialog(Order order)
    {
        InitializeComponent();
        DataContext = order;
    }

    private void Close_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as Window;
        window?.Close();
    }
}