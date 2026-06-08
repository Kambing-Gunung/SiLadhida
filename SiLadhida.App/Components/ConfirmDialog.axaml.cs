using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SiLadhida.App.Views;

public partial class ConfirmDialog : Window
{
    public ConfirmDialog(string message)
    {
        InitializeComponent();

        MessageText.Text = message;
    }

    private void Confirm_Click(
        object? sender,
        RoutedEventArgs e)
    {
        Close(true);
    }

    private void Cancel_Click(
        object? sender,
        RoutedEventArgs e)
    {
        Close(false);
    }
}