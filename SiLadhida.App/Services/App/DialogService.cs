using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using SiLadhida.App.Components.Dialogs;

namespace SiLadhida.App.Services.App;

public class DialogService
{
    private static DialogService? _instance;

    public static DialogService Instance
    {
        get
        {
            if (_instance == null)
                _instance = new DialogService();

            return _instance;
        }
    }

    private DialogService()
    {
    }

    private Window? MainWindow =>
        Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;

    public async Task<T?> ShowAsync<T>(object dialogOrContent)
    {
        if (MainWindow == null)
            return default;

        if (dialogOrContent is Window window)
        {
            return await window.ShowDialog<T>(MainWindow);
        }

        if (dialogOrContent is Control content)
        {
            var dialog = new BaseDialog();
            dialog.SetContent(content);

            return await dialog.ShowDialog<T>(MainWindow);
        }

        return default;
    }

    public async Task<bool> ConfirmAsync(string message)
    {
        var dialog = new ConfirmDialog(message);
        return await ShowAsync<bool>(dialog);
    }
}