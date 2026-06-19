using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Services;
using SiLadhida.App.Views;

namespace SiLadhida.App.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly AuthService _authService;

    [ObservableProperty]
    private string username = "";

    [ObservableProperty]
    private string password = "";

    [ObservableProperty]
    private string message = "";

    [ObservableProperty]
    private bool isPasswordVisible;

    [ObservableProperty]
    private bool isBusy;

    public LoginViewModel()
        : this(new AuthService())
    {
    }

    public LoginViewModel(AuthService authService)
    {
        _authService = authService;
    }

    [RelayCommand]
    private async Task Login()
    {
        if (IsBusy)
            return;

        Message = "";

        if (string.IsNullOrWhiteSpace(Username) ||
            string.IsNullOrWhiteSpace(Password))
        {
            Message = "Username dan Password wajib diisi.";
            return;
        }

        try
        {
            IsBusy = true;

            var success = await _authService.LoginAsync(
                Username,
                Password
            );

            if (success)
            {
                App.Navigation.Navigate(
                    new DashboardView(), "Dashboard"
                );

                return;
            }

            Message = "Username atau Password salah.";
        }
        catch (Exception)
        {
            Message = "Terjadi kesalahan saat login.";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void TogglePassword()
    {
        IsPasswordVisible = !IsPasswordVisible;
    }
}