using Avalonia.Controls;
using SiLadhida.App.Features.Dashboard;

namespace SiLadhida.App.Layouts;

public partial class AppLayout : UserControl
{
    public AppLayout()
    {
        InitializeComponent();
        DataContext = App.Services.Navigation;
    }
}