using System;
using Avalonia.Controls;
using SiLadhida.App.Features.Auth;
using SiLadhida.App.Layouts;
using SiLadhida.App.Services;
using SiLadhida.App.Services.App;

namespace SiLadhida.App;

public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();

        DataContext = App.Services.Navigation;
    }
}