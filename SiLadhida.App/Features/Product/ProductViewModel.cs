using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Components.Dialogs;
using SiLadhida.App.Services.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SiLadhida.App.Features.Product;

public partial class ProductViewModel : ObservableObject
{
    private readonly ProductService _service;

    [ObservableProperty]
    private ObservableCollection<Product> products = new();

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    public ProductViewModel()
    {
        _service = App.Services.ProductService;
    }

    private bool _initialized = false;

    public async Task InitializeAsync()
    {
        if (_initialized)
            return;

        _initialized = true;

        await LoadProductsAsync();
    }

    // 🔥 LOAD DATA
    public async Task LoadProductsAsync()
    {
        try
        {

            var data = await _service.GetProductsAsync();

            Products = new ObservableCollection<Product>(data ?? []);
        }
        catch (Exception ex)
        {
            App.Services.Notification.ShowError(ex.Message);
        }
        finally
        {
        }
    }

    // 🔥 FILTER
    public IEnumerable<Product> FilteredProducts =>
        string.IsNullOrWhiteSpace(SearchText)
            ? Products
            : Products.Where(p =>
                p.Nama.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

    public bool HasProducts => FilteredProducts.Any();

    partial void OnSearchTextChanged(string value)
    {
        OnPropertyChanged(nameof(FilteredProducts));
        OnPropertyChanged(nameof(HasProducts));
    }

    partial void OnProductsChanged(ObservableCollection<Product> value)
    {
        OnPropertyChanged(nameof(FilteredProducts));
        OnPropertyChanged(nameof(HasProducts));
    }

    [RelayCommand]
    public async Task OpenCreateDialogAsync()
    {
        if (IsLoading)
            return;

        var result = await App.Services.Dialog
            .ShowAsync<ProductFormModel>(new ProductDialog());

        if (result == null)
            return;

        try
        {
            IsLoading = true;

            var newProduct = new Product
            {
                Nama = result.Nama,
                Harga = result.Harga,
                Stock = result.Stock
            };

            var created = await _service.CreateProductAsync(newProduct);

            // 🔥 REAL-TIME ADD
           if (created != null)
            {
                // 🔥 REAL-TIME ADD
                Products.Add(created);

                OnPropertyChanged(nameof(FilteredProducts));
                OnPropertyChanged(nameof(HasProducts));

                App.Services.Notification.ShowSuccess("Produk berhasil ditambahkan");
            }
            else
            {
                App.Services.Notification.ShowError("Gagal menambahkan produk: Respons kosong dari server.");
            }
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

    [RelayCommand]
    public async Task OpenEditDialogAsync(Product product)
    {
        if (product == null || IsLoading)
            return;

        var result = await App.Services.Dialog
            .ShowAsync<ProductFormModel>(new ProductDialog(product));

        if (result == null)
            return;

        try
        {
            IsLoading = true;

            // 🔥 UPDATE LOCAL
            product.Nama = result.Nama;
            product.Harga = result.Harga;
            product.Stock = result.Stock;

            await _service.UpdateProductAsync(product);

            // 🔥 TRIGGER UI
            OnPropertyChanged(nameof(FilteredProducts));

            App.Services.NotificationPublisher.Notify("Produk berhasil diperbarui");
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

    [RelayCommand]
    public async Task DeleteProductAsync(Product product)
    {
        if (product == null || IsLoading)
            return;

        var confirm = await App.Services.Dialog.ConfirmAsync(
            $"Produk \"{product.Nama}\" akan dihapus permanen.\n\nTindakan ini tidak bisa dibatalkan."
        );

        if (!confirm)
            return;

        try
        {
            IsLoading = true;

            await _service.DeleteProductAsync(product.Id);

            // 🔥 REAL-TIME REMOVE
            Products.Remove(product);

            OnPropertyChanged(nameof(FilteredProducts));
            OnPropertyChanged(nameof(HasProducts));

            App.Services.Notification.ShowSuccess("Produk berhasil dihapus");
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
}