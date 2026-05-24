using Avalonia.Controls;
using SiLadhida.App.Views;

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

        MainContent.Content = new LoginView();
    }
}