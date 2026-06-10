using Avalonia.Controls;
using SiLadhida.App.Views;
using SiLadhida.App.Services;
using System.Threading.Tasks;

namespace SiLadhida.App;

public partial class MainWindow : Window, IObserver
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

        App.Notification.Subscribe(this);

        MainContent.Content = new LoginView();
    }

    //Aksinya
    public void UpdateNotification(string message)
    {
        ShowToast(message);
    }

    private async void ShowToast(string message)
    {
        ToastText.Text = message;
        ToastBorder.IsVisible = true;
        await Task.Delay(2500);
        ToastBorder.IsVisible = false;
    }
}