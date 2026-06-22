using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Services.Api;
using SiLadhida.App.Services.App;
using SiLadhida.App.Views;

namespace SiLadhida.App.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly AuthService _authService;
    private readonly NavigationService _navigation;

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
    {
        _authService = App.Services.AuthService;
        _navigation = App.Services.Navigation;
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

            var success = await _authService.LoginAsync(Username, Password);

            if (success)
            {
                // 🔥 Token sudah otomatis tersimpan di AuthSession
                _navigation.Navigate(new DashboardViewModel(), "Dashboard");
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