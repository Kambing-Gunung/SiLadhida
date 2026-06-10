using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SiLadhida.App.Services; 

namespace SiLadhida.App;

public partial class App : Application
{
    public static NavigationService Navigation { get; } = new NavigationService();

    public static NotificationPublisher Notification { get; } = new NotificationPublisher();

    public static SystemLogger Logger { get; } = new SystemLogger();
    public MainWindow MainWindow { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Notification.Subscribe(Logger);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            MainWindow = new MainWindow();
            desktop.MainWindow = MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}