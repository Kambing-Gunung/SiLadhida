using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SiLadhida.App.Services.App;

public partial class NavigationService : ObservableObject
{
    [ObservableProperty]
    private ObservableObject? currentViewModel;

    [ObservableProperty]
    private string title = "";

    public void Navigate(ObservableObject viewModel, string title = "")
    {
        Title = title;
        CurrentViewModel = viewModel;
    }
}