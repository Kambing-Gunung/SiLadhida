using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using Avalonia.Controls;
using SiLadhida.App.Components.Dialogs;

namespace SiLadhida.App.Services.App;

public class DialogService
{
    private Window? MainWindow =>
        Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;

    // 🔥 UNIVERSAL HANDLER
    public async Task<T?> ShowAsync<T>(object dialogOrContent)
    {
        if (MainWindow == null)
            return default;

        // ✅ CASE 1: sudah dialog (ConfirmDialog)
        if (dialogOrContent is Window window)
        {
            return await window.ShowDialog<T>(MainWindow);
        }

        // ✅ CASE 2: content (ProductDialog, dll)
        if (dialogOrContent is Control content)
        {
            var dialog = new BaseDialog();
            dialog.SetContent(content);

            return await dialog.ShowDialog<T>(MainWindow);
        }

        return default;
    }

    // 🔥 HELPER KHUSUS CONFIRM
    public async Task<bool> ConfirmAsync(string message)
    {
        var dialog = new ConfirmDialog(message);
        var result = await ShowAsync<bool>(dialog);

        return result;
    }
}