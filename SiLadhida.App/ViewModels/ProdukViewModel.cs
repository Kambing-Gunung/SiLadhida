using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Models;
using SiLadhida.App.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SiLadhida.App.ViewModels;

public partial class ProdukViewModel : ObservableObject
{
    private readonly ProductApiService _service;

    private ObservableCollection<Product> _products = new();
    public ObservableCollection<Product> Products
    {
        get => _products;
        set => SetProperty(ref _products, value);
    }

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

    [RelayCommand]
    private async Task LoadProductsAsync()
    {
        try
        {
            IsLoading = true;

            var data = await _service.GetProductsAsync();

            Products = new ObservableCollection<Product>(data ?? []);
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
    }

    [RelayCommand]
    private async Task UpdateProductAsync(Product product)
    {
        if (product == null)
            return;

        product.Harga += 5000;

        await _service.UpdateProductAsync(product);

        await LoadProductsAsync();
    }

    [RelayCommand]
    private async Task DeleteProductAsync(Product product)
    {
        if (product == null)
            return;

        await _service.DeleteProductAsync(product.Id);

        await LoadProductsAsync();
    }
}