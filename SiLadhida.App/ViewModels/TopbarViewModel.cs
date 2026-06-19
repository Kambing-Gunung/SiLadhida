using CommunityToolkit.Mvvm.ComponentModel;

namespace SiLadhida.App.ViewModels;

public partial class TopbarViewModel : ObservableObject
{
    public string _title { get; set; }

    [ObservableProperty]
    private string title = "Dashboard";

    public TopbarViewModel()
    {
        App.Navigation.OnTitleChanged += title =>
        {
            _title = title;
        };
    }
}