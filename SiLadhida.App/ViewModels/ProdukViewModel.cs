using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Models;
using SiLadhida.App.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SiLadhida.App.ViewModels;

// Kelas ViewModel untuk modul Manajemen Produk.
// fungsinya untuk  memisahkan logika bisnis dari tampilan antarmuka,
// menerapkan Observer Pattern melalui integrasi dengan NotificationPublisher.
public partial class ProdukViewModel : ObservableObject
{
    private readonly ProductApiService _service;

    // Menggunakan ObservableCollection agar antarmuka merespons perubahan data secara real-time.
    private ObservableCollection<Product> _products = new();
    public ObservableCollection<Product> Products
    {
        get => _products;
        set => SetProperty(ref _products, value);
    }

    // Indikator status pemuatan data untuk memberikan umpan balik visual kepada pengguna.
    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public ProdukViewModel()
    {
        _service = new ProductApiService();
        _ = LoadProductsAsync();
    }

    // Memuat daftar produk secara asinkron dari sumber data (API).
    // untuk mencegah berhentinya aplikasi secara paksa jika terjadi kegagalan jaringan.
    [RelayCommand]
    private async Task LoadProductsAsync()
    {
        try
        {
            IsLoading = true;
            var data = await _service.GetProductsAsync();
            Products = new ObservableCollection<Product>(data ?? []);
        }
        catch (Exception ex)
        {
            App.Notification.Notify($"[Error] Gagal memuat data produk: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    // Menyimpan data produk baru ke dalam sistem.
    // Menerapkan Observer Pattern: Setelah operasi berhasil, fungsi akan memicu 
    // Publisher untuk mengirimkan pembaruan kepada seluruh Observer.
    [RelayCommand]
    public async Task CreateProductAsync(Product product)
    {
        await _service.CreateProductAsync(product);
        await LoadProductsAsync();

        App.Notification.Notify($"[Sukses] Produk '{product.Nama}' berhasil ditambahkan ke dalam sistem.");
    }

    [RelayCommand]
    public async Task UpdateProductAsync(Product product)
    {
        if (product == null || product.Id <= 0)
        {
            App.Notification.Notify("[Error] Validasi gagal: Data produk tidak valid atau kosong.");
            return;
        }

        await _service.UpdateProductAsync(product);
        await LoadProductsAsync();

        App.Notification.Notify($"[Sukses] Data produk '{product.Nama}' berhasil diperbarui.");
    }

    // Menghapus data produk dari basis data berdasarkan id
    [RelayCommand]
    public async Task DeleteProductAsync(Product product)
    {
        if (product == null || product.Id <= 0)
        {
            App.Notification.Notify("[Error] Validasi gagal: Data produk tidak ditemukan.");
            return;
        }

        await _service.DeleteProductAsync(product.Id);
        await LoadProductsAsync();

        App.Notification.Notify($"[Sukses] Produk '{product.Nama}' berhasil dihapus dari sistem.");
    }
}