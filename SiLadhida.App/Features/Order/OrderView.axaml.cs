using Avalonia.Controls;
using Avalonia.Interactivity;
using SiLadhida.App.Components.Dialogs;

namespace SiLadhida.App.Features.Order;

public partial class OrderView : UserControl
{
    public OrderView()
    {
        InitializeComponent();
    }

    private async void Manage_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;
        if (button.Tag is not Order order) return;

        var dialog = new OrderDialog(order);

        // Jika arsitektur Anda sekarang menggunakan BaseDialog, gunakan cara ini:
        var baseDialog = new BaseDialog();
        dialog.AttachDialog(baseDialog);
        baseDialog.SetContent(dialog);

        await App.Services.Dialog.ShowAsync<object?>(baseDialog);

        if (DataContext is OrderViewModel vm)
        {
            await vm.LoadOrdersAsync();
        }
    }
}