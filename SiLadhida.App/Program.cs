using Avalonia;
using System;
using System.IO;

namespace SiLadhida.App;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        // 🔥 Tangkap error yang bikin app close mendadak -> tulis ke Desktop
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            LogCrash(e.ExceptionObject as Exception);

        try
        {
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            LogCrash(ex);
            throw;
        }
    }

    private static void LogCrash(Exception? ex)
    {
        try
        {
            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "siladhida-crash.txt");
            File.WriteAllText(path, DateTime.Now + "\n\n" + (ex?.ToString() ?? "Unknown error"));
        }
        catch { }
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}