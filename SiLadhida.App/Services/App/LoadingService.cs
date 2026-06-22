using CommunityToolkit.Mvvm.ComponentModel;

namespace SiLadhida.App.Services.App;

public partial class LoadingService : ObservableObject
{
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