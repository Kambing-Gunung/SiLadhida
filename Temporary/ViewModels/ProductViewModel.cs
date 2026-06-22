using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Models;
using SiLadhida.App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SiLadhida.App.ViewModels;

public partial class ProductViewModel : ObservableObject
{
    private readonly ProductApiService _service;

    [ObservableProperty]
    private ObservableCollection<Product> products = new();

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private bool isLoading;

    public ProductViewModel()
    {
        _service = new();
    }

    // 🔥 LOAD DATA
    public async Task LoadProductsAsync()
    {
        try
        {
            IsLoading = true;

            var data = await _service.GetProductsAsync();

            Products = new ObservableCollection<Product>(data ?? []);
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

    // 🔥 CREATE
    [RelayCommand]
    public async Task CreateProductAsync(Product product)
    {
        try
        {
            await _service.CreateProductAsync(product);

            await LoadProductsAsync();

            App.Notification.ShowSuccess("Produk berhasil ditambahkan");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    // 🔥 UPDATE
    [RelayCommand]
    public async Task UpdateProductAsync(Product product)
    {
        try
        {
            await _service.UpdateProductAsync(product);

            await LoadProductsAsync();

            App.Notification.ShowSuccess("Produk berhasil diupdate");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }

    // 🔥 DELETE
    [RelayCommand]
    public async Task DeleteProductAsync(Product product)
    {
        if (product == null)
            return;

        try
        {
            await _service.DeleteProductAsync(product.Id);

            await LoadProductsAsync();

            App.Notification.ShowSuccess("Produk berhasil dihapus");
        }
        catch (Exception ex)
        {
            App.Notification.ShowError(ex.Message);
        }
    }
}