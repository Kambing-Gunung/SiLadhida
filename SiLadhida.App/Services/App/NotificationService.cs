using CommunityToolkit.Mvvm.ComponentModel;
using SiLadhida.App.Services.Observer;
using System.Threading.Tasks;

namespace SiLadhida.App.Services.App;

public partial class NotificationService : ObservableObject, IObserver
{
    private static NotificationService? _instance;

    public static NotificationService Instance
    {
        get
        {
            if (_instance == null)
                _instance = new NotificationService();

            return _instance;
        }
    }

    private NotificationService()
    {
    }

    [ObservableProperty]
    private string message = "";

    [ObservableProperty]
    private bool isVisible;

    [ObservableProperty]
    private bool isError;

    public async void UpdateNotification(string message)
    {
        Message = message;
        IsError = false;
        IsVisible = true;

        await Task.Delay(2500);

        IsVisible = false;
    }

    public async void ShowSuccess(string message)
    {
        Message = message;
        IsError = false;
        IsVisible = true;

        await Task.Delay(2500);

        IsVisible = false;
    }

    public async void ShowError(string message)
    {
        Message = message;
        IsError = true;
        IsVisible = true;

        await Task.Delay(3000);

        IsVisible = false;
    }
}