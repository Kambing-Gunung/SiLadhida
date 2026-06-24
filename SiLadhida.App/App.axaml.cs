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
        // 🔥 INIT SERVICES
        Services = new ServiceLocator();
        Services.Initialize();
        Notification.Subscribe(Logger);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // 🔥 SET ROOT WINDOW
            desktop.MainWindow = new MainWindow
            {
                DataContext = Services.Navigation
            };

            // 🔥 START FROM LOGIN
            Services.Navigation.NavigateToLogin();
        }

        base.OnFrameworkInitializationCompleted();
    }
}