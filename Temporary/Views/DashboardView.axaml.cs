using Avalonia.Controls;
using SiLadhida.App.ViewModels;

namespace SiLadhida.App.Views;

public partial class DashboardView : UserControl
{
    public DashboardView()
{
    InitializeComponent();

    App.Navigation.SetTitle("Dashboard");

    if (DataContext is DashboardViewModel vm)
    {
        _ = vm.LoadAsync();
    }

    // TEMPORARY TEST
    App.Navigation.Navigate(new OrderView(), "Order");
}
}