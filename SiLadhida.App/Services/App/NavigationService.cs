using CommunityToolkit.Mvvm.ComponentModel;
using SiLadhida.App.Features.Auth;
using SiLadhida.App.Features.Dashboard;
using SiLadhida.App.Features.Product;
using SiLadhida.App.Features.Order;
using SiLadhida.App.Features.Transaction;
using SiLadhida.App.Layouts;

namespace SiLadhida.App.Services.App;

public partial class NavigationService : ObservableObject
{
    [ObservableProperty]
    private object? currentView;

    [ObservableProperty]
    private object? currentContent;

    [ObservableProperty]
    private string title = "";

    public void NavigateToLogin()
    {
        CurrentView = new LoginViewModel();
        CurrentContent = null;
        Title = "Login";
    }

    public void NavigateToShell(object content, string title)
    {
        CurrentView = new AppLayout();
        CurrentContent = content;
        Title = title;
    }

    public void NavigateContent(object content, string title)
    {
        CurrentContent = content;
        Title = title;
    }
}