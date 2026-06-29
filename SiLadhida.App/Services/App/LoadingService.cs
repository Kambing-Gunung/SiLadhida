using CommunityToolkit.Mvvm.ComponentModel;

namespace SiLadhida.App.Services.App;

public partial class LoadingService : ObservableObject
{
    private static LoadingService? _instance;

    public static LoadingService Instance
    {
        get
        {
            if (_instance == null)
                _instance = new LoadingService();

            return _instance;
        }
    }

    private LoadingService()
    {
    }

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string message = "Memuat...";

    public void Show(string? message = null)
    {
        if (!string.IsNullOrWhiteSpace(message))
            Message = message;

        IsLoading = true;
    }

    public void Hide()
    {
        IsLoading = false;
    }
}