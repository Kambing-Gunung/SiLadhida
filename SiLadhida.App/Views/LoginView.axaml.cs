using Avalonia.Controls;
using SiLadhida.App.ViewModels;

namespace SiLadhida.App.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();

        DataContext = new LoginViewModel();
    }
}