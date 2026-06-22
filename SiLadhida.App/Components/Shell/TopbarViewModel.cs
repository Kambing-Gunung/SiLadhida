using CommunityToolkit.Mvvm.ComponentModel;
using SiLadhida.App.Services.App;

namespace SiLadhida.App.Components.Shell;

public partial class TopbarViewModel : ObservableObject
{
    private readonly NavigationService _navigation;

    public TopbarViewModel()
    {
        _navigation = App.Services.Navigation;

        _navigation.PropertyChanged += (_, __) =>
        {
            OnPropertyChanged(nameof(Title));
        };
    }

    public string Title => _navigation.Title;
}