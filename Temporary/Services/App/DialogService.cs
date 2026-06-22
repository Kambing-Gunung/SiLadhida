using System.Threading.Tasks;
using Avalonia.Controls;
using SiLadhida.App.Components.Dialogs;

namespace SiLadhida.App.Services.App;

public class DialogService
{
    public async Task ShowOrderDialog(int orderId)
    {
        var dialog = new OrderDialog(orderId);

        if (App.MainWindow != null)
        {
            await dialog.ShowDialog(App.MainWindow);
        }
    }

    public async Task ShowProductDialog()
    {
        var dialog = new ProductDialog();

        if (App.MainWindow != null)
        {
            await dialog.ShowDialog(App.MainWindow);
        }
    }

    public async Task<bool> ShowConfirm(string message)
    {
        var dialog = new ConfirmDialog(message);

        if (App.MainWindow != null)
        {
            var result = await dialog.ShowDialog<bool>(App.MainWindow);
            return result;
        }

        return false;
    }
}