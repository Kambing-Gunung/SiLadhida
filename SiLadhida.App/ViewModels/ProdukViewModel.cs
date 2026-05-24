using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SiLadhida.App.Models;
using SiLadhida.App.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SiLadhida.App.Views;

namespace SiLadhida.App.ViewModels;

public partial class ProdukViewModel : ObservableObject
{
    private readonly ProductApiService _service;

    [ObservableProperty]
    private ObservableCollection<Product> products = new();

    public ProdukViewModel()
    {
        _service = new ProductApiService();

        _ = LoadProductsAsync();
    }

    [RelayCommand]
    private async Task LoadProductsAsync()
    {
        var data = await _service.GetProductsAsync();

        Products = new ObservableCollection<Product>(data);
    }

    [RelayCommand]
    private async Task CreateProductAsync()
    {
        var dialog = new ProductDialog();

        var result = await dialog.ShowDialog<ProductFormModel?>(
            App.MainWindow!
        );

        if (result == null)
            return;

        var product = new Product
        {
            Nama = result.Nama,
            Harga = result.Harga,
            Stock = result.Stock
        };

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