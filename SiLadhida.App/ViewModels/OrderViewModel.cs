using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Models;
using SiLadhida.App.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SiLadhida.App.ViewModels;

public partial class OrderViewModel
    : ObservableObject
{
    private readonly OrderApiService _service;

    [ObservableProperty]
    private ObservableCollection<Order> orders = new();

    [ObservableProperty]
    private Order? selectedOrder;

    [ObservableProperty]
    private bool isLoading;

    public OrderViewModel()
    {
        _service = new();

        _ = LoadOrdersAsync();
    }

    [RelayCommand]
    private async Task LoadOrdersAsync()
    {
        try
        {
            IsLoading = true;

            var data =
                await _service.GetOrdersAsync();

            Orders =
                new ObservableCollection<Order>(
                    data ?? []);
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(
                ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadOrdersAsync();
    }
}