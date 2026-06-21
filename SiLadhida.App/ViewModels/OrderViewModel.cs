using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Models;
using SiLadhida.App.Models.Requests;
using SiLadhida.App.Services;
using SiLadhida.Core.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using SiLadhida.App.Views;

namespace SiLadhida.App.ViewModels;

public partial class OrderViewModel : ObservableObject
{
    // 1. Properti untuk SearchKeyword
    private string _searchKeyword = string.Empty;
    public string SearchKeyword
    {
        get => _searchKeyword;
        // Sesuaikan cara update propertinya dengan MVVM library yang kamu gunakan
        // Jika pakai ReactiveUI: set => this.RaiseAndSetIfChanged(ref _searchKeyword, value);
        // Jika pakai CommunityToolkit: set { SetProperty(ref _searchKeyword, value); }
        set 
        {
            _searchKeyword = value;
            // Panggil pemberitahuan perubahan UI di sini
        }
    }

    // 2. Properti untuk List Pesanan Selesai
    public ObservableCollection<Order> CompletedOrders { get; } = new ObservableCollection<Order>();

    // 3. Command untuk tombol "+ Buat Pesanan Baru"
    // public ICommand OpenCreateOrderDialogCommand { get; }

    // Pastikan di dalam Constructor OrderViewModel() kamu menginisialisasi Command-nya
    // Contoh (tergantung kerangka kerjamu):
    // OpenCreateOrderDialogCommand = ReactiveCommand.Create(BukaDialogPesananBaru);
    private readonly OrderApiService _service;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ActiveOrders))] // untuk menampilkan pesanan yang masih aktif
    [NotifyPropertyChangedFor(nameof(CompletedOrders))] // <-- untuk menampilkan riwayat pesanan selesai
    private ObservableCollection<Order> orders = new();

    [ObservableProperty]
    private Order? selectedOrder;

    [ObservableProperty]
    private string namaPemesan = string.Empty;

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

            var data = await _service.GetOrdersAsync();

            Orders = new ObservableCollection<Order>(data ?? []);
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

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadOrdersAsync();
    }

    [RelayCommand]
    private async Task CreateOrderAsync()
    {
        if (string.IsNullOrWhiteSpace(NamaPemesan))
        {
            App.Notification.ShowError("Nama pemesan wajib diisi.");

            return;
        }

        try
        {
            var request = new CreateOrderRequest
            {
                NamaPemesan = NamaPemesan
            };

            await _service.CreateOrderAsync(request);

            NamaPemesan = string.Empty;

            await LoadOrdersAsync();
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task PayOrderAsync(Order order)
    {
        if (order == null)
            return;

        try
        {
            await _service.PayAsync(order.Id);

            await LoadOrdersAsync();

            App.Notification.ShowSuccess("Pembayaran berhasil");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task CancelOrderAsync(Order order)
    {
        if (order == null)
            return;

        try
        {
            await _service.CancelAsync(order.Id);

            await LoadOrdersAsync();

            App.Notification.ShowSuccess("Order dibatalkan");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task DeleteOrderAsync(Order order)
    {
        if (order == null)
            return;

        try
        {
            // Memanggil API Delete
            await _service.DeletedOrderAsync(order.Id);

            // Memuat ulang daftar pesanan agar data 
            // yang sudah dihapus hilang dari data pesanan
            await LoadOrdersAsync();
            App.Notification.ShowSuccess("Data pesanan berhasil dihapus");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task CompleteOrderAsync(Order order)
    {
        if (order == null)
            return;

        try
        {
            await _service.CompleteAsync(order.Id);

            await LoadOrdersAsync();

            App.Notification.ShowSuccess("Pesanan selesai");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    [RelayCommand]
    private async Task OpenCreateOrderDialogAsync()
    {
        var desktop = Avalonia.Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;

        if (desktop != null && desktop.MainWindow != null)
        {
            // Buka form dialog
            var dialog = new SiLadhida.App.Views.CreateOrderDialog();
            var result = await dialog.ShowDialog<bool>(desktop.MainWindow);

            // Jika user menekan tombol "Simpan & Buat Pesanan" dan validasi lolos
            if (result)
            {
                try
                {
                    IsLoading = true;

                    // 1. Tembak API untuk membuat Pesanan Baru (Hanya mengirim Nama)
                    var request = new CreateOrderRequest { NamaPemesan = dialog.NewOrderName };
                    var createdOrder = await _service.CreateOrderAsync(request);

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
                            await _service.AddItemAsync(createdOrder.Id, addItemReq);
                        }

                        SiLadhida.App.App.Notification.ShowSuccess("Pesanan berhasil dibuat!");
                    }

                    // 4. Perbarui UI Halaman Utama agar pesanan langsung muncul
                    await LoadOrdersAsync();
                }
                catch (Exception ex)
                {
                    SiLadhida.App.App.Notification.ShowError("Gagal membuat pesanan: " + ex.Message);
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }
    }

    public ObservableCollection<Order> ActiveOrders => new(Orders.Where(o =>
        o.StatusSekarang == StateOrder.MenungguPembayaran ||
        o.StatusSekarang == StateOrder.SiapDiambil));
    
    // public ObservableCollection<Order> CompletedOrders => new(Orders.Where(o =>
    //     o.StatusSekarang == StateOrder.Selesai ||
    //     o.StatusSekarang == StateOrder.Dibatalkan));
}