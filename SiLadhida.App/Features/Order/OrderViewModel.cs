using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Components.Dialogs;
using SiLadhida.App.Services.Api;
using SiLadhida.Core.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SiLadhida.App.Features.Order;

public partial class OrderViewModel : ObservableObject
{
    private readonly OrderService _service;

    [ObservableProperty]
    private ObservableCollection<Order> orders = new();

    [ObservableProperty]
    private string namaPemesan = string.Empty;

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isBusy = false;

    public OrderViewModel()
    {
        _service = App.Services.OrderService;

        _ = LoadOrdersAsync();
    }

    // 🔥 LOAD DATA
    public async Task LoadOrdersAsync()
    {
        try
        {
            IsLoading = true;

            var data = await _service.GetOrdersAsync();

            Orders = new ObservableCollection<Order>(data ?? []);

            OnPropertyChanged(nameof(ActiveOrders));
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

    // 🔥 FILTER + SEARCH
    public IEnumerable<Order> ActiveOrders =>
        Orders
            .Where(o =>
                o.StatusSekarang == StateOrder.MenungguPembayaran ||
                o.StatusSekarang == StateOrder.SiapDiambil)
            .Where(o =>
                string.IsNullOrWhiteSpace(SearchText) ||
                o.NamaPemesan.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

    partial void OnSearchTextChanged(string value)
    {
        OnPropertyChanged(nameof(ActiveOrders));
    }

    partial void OnOrdersChanged(ObservableCollection<Order> value)
    {
        OnPropertyChanged(nameof(ActiveOrders));
    }

    // 🔥 CREATE ORDER
    [RelayCommand]
    private async Task CreateOrderAsync()
    {
        if (string.IsNullOrWhiteSpace(NamaPemesan))
        {
            App.Services.Notification.ShowError("Nama pemesan wajib diisi.");
            return;
        }

        if (IsBusy) return;

        try
        {
            IsBusy = true;

            App.Services.Loading.Show("Membuat pesanan...");

            await _service.CreateOrderAsync(new Shared.Requests.CreateOrderRequest
            {
                NamaPemesan = NamaPemesan
            });

            NamaPemesan = string.Empty;

            await LoadOrdersAsync();

            App.Services.Notification.ShowSuccess("Order berhasil dibuat");
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
        finally
        {
            App.Services.Loading.Hide();
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task OpenManageDialogAsync(Order order)
    {
        if (order == null || IsBusy)
            return;

        try
        {
            IsBusy = true;

            var content = new OrderDialog(order);

            var dialog = new BaseDialog();

            // 🔥 INJECT DIALOG KE CONTENT
            content.AttachDialog(dialog);

            dialog.SetContent(content);

            await App.Services.Dialog.ShowAsync<object?>(dialog);

            await LoadOrdersAsync();
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    // 🔥 PAY
    [RelayCommand]
    private async Task PayOrderAsync(Order order)
    {
        if (order == null || IsBusy) return;

        try
        {
            IsBusy = true;

            App.Services.Loading.Show("Memproses pembayaran...");

            await _service.PayAsync(order.Id);

            await LoadOrdersAsync();

            App.Services.Notification.ShowSuccess("Pembayaran berhasil");
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
        finally
        {
            App.Services.Loading.Hide();
            IsBusy = false;
        }
    }

    // 🔥 CANCEL
    [RelayCommand]
    private async Task CancelOrderAsync(Order order)
    {
        if (order == null || IsBusy) return;

        try
        {
            IsBusy = true;

            App.Services.Loading.Show("Membatalkan pesanan...");

            await _service.CancelAsync(order.Id);

            await LoadOrdersAsync();

            App.Services.Notification.ShowSuccess("Order dibatalkan");
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
        finally
        {
            App.Services.Loading.Hide();
            IsBusy = false;
        }
    }

    // 🔥 COMPLETE
    [RelayCommand]
    private async Task CompleteOrderAsync(Order order)
    {
        if (order == null || IsBusy) return;

        try
        {
            IsBusy = true;

            App.Services.Loading.Show("Menyelesaikan pesanan...");

            await _service.CompleteAsync(order.Id);

            await LoadOrdersAsync();

            App.Services.Notification.ShowSuccess("Pesanan selesai");
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
        finally
        {
            App.Services.Loading.Hide();
            IsBusy = false;
        }
    }
}