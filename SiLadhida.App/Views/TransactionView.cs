using Avalonia.Controls;
using Avalonia.Input;
using SiLadhida.App.Models;
using SiLadhida.App.Components.Dialogs;

namespace SiLadhida.App.Views;

public partial class TransactionView : UserControl
{
    public TransactionView()
    {
        InitializeComponent();
    }

    private async void OpenDetail_Click(object? sender, PointerPressedEventArgs e)
    {
        if (sender is not Border border)
            return;

        if (border.Tag is not Order order)
            return;

        var dialog = new TransactionDialog(order);

        var host = new Window
        {
            Content = dialog,
            Width = 500,
            Height = 600,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            WindowDecorations = WindowDecorations.None
        };

        var mainWindow = TopLevel.GetTopLevel(this) as Window;

        if (mainWindow == null)
            return;

        await host.ShowDialog(mainWindow);
    }
}