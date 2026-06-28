using CommunityToolkit.Mvvm.ComponentModel;
using SiLadhida.App.Services.Api;
using SiLadhida.Core.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using OrderEntity = SiLadhida.App.Features.Order.Order;

namespace SiLadhida.App.Features.Transaction;

public partial class TransactionViewModel : ObservableObject
{
    private readonly OrderService _service;

    [ObservableProperty]
    private ObservableCollection<OrderEntity> orders = new();

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    public TransactionViewModel()
    {
        _service = App.Services.OrderService;

        _ = LoadTransactionsAsync();
    }

    public IEnumerable<OrderEntity> Transactions => Orders
        .Where(o =>
            o.StatusSekarang == StateOrder.Selesai ||
            o.StatusSekarang == StateOrder.Dibatalkan)
        .Where(o =>
            string.IsNullOrWhiteSpace(SearchText) ||
            o.NamaPemesan.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

    public async Task LoadTransactionsAsync()
    {
        try
        {
            IsLoading = true;

            var data = await _service.GetOrdersAsync();

            Orders = new ObservableCollection<OrderEntity>(data ?? []);

            OnPropertyChanged(nameof(Transactions));
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        OnPropertyChanged(nameof(Transactions));
    }
}
