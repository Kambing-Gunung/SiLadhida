using Avalonia.Controls;

namespace SiLadhida.App.Views;

public partial class DashboardView : UserControl
{
    public DashboardView()
    {
        InitializeComponent();

        App.Navigation.SetTitle("Dashboard");
    }
}