using Avalonia.Controls;
using SiLadhida.App.Views;

namespace SiLadhida.App;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        App.Navigation.OnViewChanged += view =>
        {
            MainContent.Content = view;
        };

        MainContent.Content = new DashboardView();
    }
}