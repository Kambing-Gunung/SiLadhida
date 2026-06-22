using Avalonia.Controls;
using SiLadhida.App.Features.Auth;
using SiLadhida.App.Services.App;

namespace SiLadhida.App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = App.Services.Navigation;

        // App.Services.Notification.OnSuccess += ShowToast;
        // App.Services.Notification.OnError += ShowToast;

        // 🔥 Initial Page
        App.Services.Navigation.NavigateToLogin();
    }

    // private async void ShowToast(string message)
    // {
    //     ToastText.Text = message;

    //     ToastBorder.IsVisible = true;

    //     await Task.Delay(2500);

    //     ToastBorder.IsVisible = false;
    // }
}