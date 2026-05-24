using Avalonia.Controls;

namespace SiLadhida.App.Views;

public partial class DashboardLayout : UserControl
{
    public DashboardLayout()
    {
        InitializeComponent();

        DashboardContent.Content = new DashboardView();
    }

    public void SetContent(UserControl view)
    {
        DashboardContent.Content = view;
    }
}