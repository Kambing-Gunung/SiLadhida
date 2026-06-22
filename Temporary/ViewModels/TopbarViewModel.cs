using CommunityToolkit.Mvvm.ComponentModel;

namespace SiLadhida.App.ViewModels;

public partial class TopbarViewModel : ObservableObject
{
    [ObservableProperty]
    private string title = "Dashboard";

    public TopbarViewModel()
    {
        App.Navigation.OnTitleChanged += title =>
        {
            Title = title;
        };
    }
}