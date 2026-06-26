using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.Core.Enums;
using SiLadhida.App.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SiLadhida.App.Features.Order;

public partial class OrderViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActiveOrders))]
    [NotifyPropertyChangedFor(nameof(CompletedOrders))]
    private ObservableCollection<Order> orders = new();

    [ObservableProperty]
    private bool isLoading;

    public OrderViewModel()
    {
        _ = LoadOrdersAsync();
    }

    [RelayCommand]
    public async Task LoadOrdersAsync()
    {
        try
        {
            IsLoading = true;
            var rawData = await App.Services.OrderService.GetOrdersAsync();

            // 1. Filter awal: Buang pesanan yang Dibatalkan agar benar-benar hilang dari UI
            var validData = rawData?.Where(o => o.StatusSekarang != StateOrder.Dibatalkan).ToList();

            Orders = new ObservableCollection<Order>(validData ?? []);
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

    // 2. Pesanan Aktif: Tampilkan semua pesanan selama statusnya BUKAN Selesai
    public IEnumerable<Order> ActiveOrders => Orders.Where(o => 
        o.StatusSekarang != StateOrder.Selesai);

    // 3. Riwayat Selesai: Tampilkan HANYA pesanan yang statusnya Selesai
    public IEnumerable<Order> CompletedOrders => Orders.Where(o => 
        o.StatusSekarang == StateOrder.Selesai);

    [RelayCommand]
    private async Task OpenCreateOrderDialogAsync()
    {
        var desktop = Avalonia.Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;

        if (desktop?.MainWindow != null)
        {
            var dialog = new CreateOrderDialog(); // Memanggil dialog form pesanan
            var result = await dialog.ShowDialog<bool>(desktop.MainWindow);

            if (result)
            {
                try
                {
                    App.Services.Loading.Show("Menyimpan pesanan...");

                    // 1. Tembak API untuk membuat Pesanan Baru (Hanya mengirim Nama Pemesan)
                    var request = new CreateOrderRequest { NamaPemesan = dialog.NewOrderName };
                    var createdOrder = await App.Services.OrderService.CreateOrderAsync(request);

                    if (createdOrder != null)
                    {
                        // 2. Ambil produk yang dicentang di form dialog
                        var selectedItems = dialog.ProductList.Where(p => p.IsSelected).ToList();

                        // 3. Tembak API AddItem untuk masing-masing produk ke OrderId yang baru dibuat
                        foreach (var item in selectedItems)
                        {
                            var addItemReq = new AddOrderItemRequest 
                            { 
                                ProductId = item.ProductId, 
                                Quantity = item.Quantity 
                            };
                            
                            // Memasukkan item yang akan memicu backend menghitung ulang TotalHarga
                            await App.Services.OrderService.AddItemAsync(createdOrder.Id, addItemReq);
                        }

                        App.Services.Notification.ShowSuccess("Pesanan berhasil dibuat!");
                    }

                    // 4. Perbarui UI Halaman Utama agar pesanan muncul dengan TotalHarga yang sudah dihitung backend
                    await LoadOrdersAsync();
                }
                catch (Exception ex)
                {
                    App.Services.Notification.ShowError("Gagal membuat pesanan: " + ex.Message);
                }
                finally
                {
                    App.Services.Loading.Hide();
                }
            }
        }
    }

    [RelayCommand]
    private async Task PayOrderAsync(Order order)
    {
        if (order == null) return;
        try
        {
            App.Services.Loading.Show("Memproses pembayaran...");
            await App.Services.OrderService.PayAsync(order.Id);
            await LoadOrdersAsync();
            App.Services.Notification.ShowSuccess("Pembayaran berhasil");
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
        finally { App.Services.Loading.Hide(); }
    }

    [RelayCommand]
    private async Task CancelOrderAsync(Order order)
    {
        if (order == null) return;
        try
        {
            await App.Services.OrderService.CancelAsync(order.Id);
            await LoadOrdersAsync();
            App.Services.Notification.ShowSuccess("Order dibatalkan");
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task CompleteOrderAsync(Order order)
    {
        if (order == null) return;
        try
        {
            await App.Services.OrderService.CompleteAsync(order.Id);
            await LoadOrdersAsync();
            App.Services.Notification.ShowSuccess("Pesanan selesai");
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
    }

    // public IEnumerable<Order> ActiveOrders => Orders.Where(o =>
    //     o.StatusSekarang == StateOrder.MenungguPembayaran ||
    //     o.StatusSekarang == StateOrder.SiapDiambil);

    // public IEnumerable<Order> CompletedOrders => Orders.Where(o =>
    //     o.StatusSekarang == StateOrder.Selesai ||
    //     o.StatusSekarang == StateOrder.Dibatalkan);
}