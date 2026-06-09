using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace SiLadhida.App.ViewModels;

public partial class TopbarViewModel : ObservableObject
{
    private string _title;
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public TopbarViewModel()
    {
        _title = "Ini Topbar";
    }
}