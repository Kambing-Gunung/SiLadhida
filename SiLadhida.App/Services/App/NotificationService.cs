using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SiLadhida.App.Services.App;

public partial class NotificationService : ObservableObject
{
    [ObservableProperty]
    private string message = "";

    [ObservableProperty]
    private bool isVisible;

    [ObservableProperty]
    private bool isError;

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