using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Services;
using SiLadhida.App.Views;

namespace SiLadhida.App.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly AuthService _authService;

    [ObservableProperty]
    private string username = "";

    [ObservableProperty]
    private string password = "";

    [ObservableProperty]
    private string message = "";

    public LoginViewModel()
    {
        _authService = new AuthService();
    }

    [RelayCommand]
    private async Task Login()
    {
        var success = await _authService.LoginAsync(
            Username,
            Password
        );

        if (success)
        {
            Message = "Login berhasil";

            App.Navigation.Navigate(
                new DashboardView()
            );
        }
        else
        {
            Message = "Login gagal";
        }
    }
}