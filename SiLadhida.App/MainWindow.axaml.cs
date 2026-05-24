using Avalonia.Controls;
using SiLadhida.App.Views;
using System.Threading.Tasks;

namespace SiLadhida.App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var layout = new DashboardLayout();

        MainContent.Content = layout;

        App.Navigation.OnViewChanged += view =>
        {
            if (view is LoginView)
            {
                MainContent.Content = view;
            }
            else
            {
                layout.SetContent(view);
                MainContent.Content = layout;
            }
        };

        App.Notification.OnSuccess += ShowToast;
        App.Notification.OnError += ShowToast;

        MainContent.Content = new LoginView();
    }

    private async void ShowToast(string message)
    {
        ToastText.Text = message;

        ToastBorder.IsVisible = true;

        await Task.Delay(2500);

        ToastBorder.IsVisible = false;
    }
}