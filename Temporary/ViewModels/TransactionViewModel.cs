using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Models;
using SiLadhida.App.Services;
using SiLadhida.Core.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SiLadhida.App.ViewModels;

public partial class TransactionViewModel : ObservableObject
{
    private readonly OrderApiService _service;

    [ObservableProperty]
    private ObservableCollection<Order> transactions = new();

    [ObservableProperty]
    private bool isLoading;

    public TransactionViewModel()
    {
        _service = new();

        _ = LoadTransactionsAsync();
    }

    [RelayCommand]
    private async Task LoadTransactionsAsync()
    {
        try
        {
            IsLoading = true;

            var data = await _service.GetOrdersAsync();

            var filtered = data?
                .Where(o =>
                    o.StatusSekarang == StateOrder.Selesai ||
                    o.StatusSekarang == StateOrder.Dibatalkan)
                .ToList();

            Transactions = new ObservableCollection<Order>(filtered ?? []);
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }
}