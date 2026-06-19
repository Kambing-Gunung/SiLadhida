using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Models;
using SiLadhida.App.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace SiLadhida.App.ViewModels;

public partial class ProductViewModel : ObservableObject
{
    private readonly ProductApiService _service;

    public bool HasProducts => FilteredProducts.Any();

    private ObservableCollection<Product> _products = new();
    public ObservableCollection<Product> Products
    {
        get => _products;
        set
        {
            SetProperty(ref _products, value);
            ApplyFilter();
        }
    }

    [ObservableProperty]
    private string searchText = "";

    partial void OnSearchTextChanged(string value)
    {
        ApplyFilter();
    }

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private ObservableCollection<Product> _filteredProducts = new();
    public ObservableCollection<Product> FilteredProducts
    {
        get => _filteredProducts;
        set => SetProperty(ref _filteredProducts, value);
    }

    private void ApplyFilter()
    {
        IEnumerable<Product> result;

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            result = Products;
        }
        else
        {
            result = Products.Where(p =>
                (p.Nama ?? "").Contains(SearchText, StringComparison.OrdinalIgnoreCase)
                || p.Harga.ToString().Contains(SearchText)
                || p.Stock.ToString().Contains(SearchText)
            );
        }

        FilteredProducts.Clear();

        foreach (var item in result)
            FilteredProducts.Add(item);

        OnPropertyChanged(nameof(HasProducts));
    }

    public ProductViewModel()
    {
        _service = new ProductApiService();

        _ = Task.Run(LoadProductsAsync);
    }

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
            App.Notification.ShowError(ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task CreateProductAsync(Product product)
    {
        await _service.CreateProductAsync(product);

        await LoadProductsAsync();

        App.Notification.ShowSuccess("Produk berhasil ditambahkan");
    }

    [RelayCommand]
    public async Task UpdateProductAsync(Product product)
    {
        if (product == null || product.Id <= 0)
        {
            App.Notification.ShowError("Produk tidak valid");
            return;
        }

        await _service.UpdateProductAsync(product);

        await LoadProductsAsync();

        App.Notification.ShowSuccess("Produk berhasil diperbarui");
    }

    [RelayCommand]
    public async Task DeleteProductAsync(Product product)
    {
        if (product == null || product.Id <= 0)
        {
            App.Notification.ShowError("Produk tidak valid");
            return;
        }

        await _service.DeleteProductAsync(product.Id);

        await LoadProductsAsync();

        App.Notification.ShowSuccess("Produk berhasil dihapus");
    }
}