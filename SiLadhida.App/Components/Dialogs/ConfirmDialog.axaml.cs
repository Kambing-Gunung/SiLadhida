using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace SiLadhida.App.Components.Dialogs;

public partial class ConfirmDialog : UserControl
{
    public string Message { get; set; } = "";

    public ConfirmDialog()
    {
        InitializeComponent();
        DataContext = this;
    }

    public ConfirmDialog(string message)
    {
        InitializeComponent();
        Message = message;
        DataContext = this;
    }

    private async void Confirm_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = this.FindAncestorOfType<BaseDialog>();

        if (dialog != null)
            await dialog.CloseWithAnimation(true);
    }

    private async void Cancel_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = this.FindAncestorOfType<BaseDialog>();

        if (dialog != null)
            await dialog.CloseWithAnimation(false);
    }
}