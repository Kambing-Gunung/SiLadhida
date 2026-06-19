using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using SiLadhida.App.ViewModels;

namespace SiLadhida.App.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();

        DataContext = new LoginViewModel();

        Dispatcher.UIThread.Post(() =>
    {
        UsernameTextBox.Focus();
    });
    }

    private void OnLoginKeyDown(
    object? sender,
    KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
            return;

        if (DataContext is LoginViewModel vm &&
            vm.LoginCommand.CanExecute(null))
        {
            vm.LoginCommand.Execute(null);
        }
    }
}