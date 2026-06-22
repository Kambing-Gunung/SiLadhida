using CommunityToolkit.Mvvm.ComponentModel;

namespace SiLadhida.App.Services.App;

public partial class LoadingService : ObservableObject
{
    [ObservableProperty]
    private bool isLoading;

    public void Show()
    {
        IsLoading = true;
    }

    public void Hide()
    {
        IsLoading = false;
    }
}