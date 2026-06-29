using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SiLadhida.App.Services;
using SiLadhida.App.Services.App;

namespace SiLadhida.App;

public partial class App : Application
{
    public static ServiceLocator Services { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Services = new ServiceLocator();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Services.Navigation
            };

            Services.Navigation.NavigateToLogin();
        }

        base.OnFrameworkInitializationCompleted();
    }
}