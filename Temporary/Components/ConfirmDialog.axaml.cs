using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace SiLadhida.App.Components;

public partial class ConfirmDialog : Window
{
    public ConfirmDialog(string message)
    {
        InitializeComponent();

        MessageText.Text = message;
    }

    private void Confirm_Click(object? sender, RoutedEventArgs e)
    {
        Close(true);
    }

    private void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }

    private void BeginMoveDrag(object? sender, PointerPressedEventArgs e)
    {
        base.BeginMoveDrag(e); 
    }
}