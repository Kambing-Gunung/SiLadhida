using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// alias
using ProductEntity = SiLadhida.App.Features.Product.Product;

namespace SiLadhida.App.Features.Order;

public partial class ProductPickerViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ProductEntity> products = new();

    public ObservableCollection<ProductEntity> SelectedProducts { get; } = new();

    // 🔥 EVENT (GANTI BaseDialog)
    public event Action<object?>? RequestClose;

    public ProductPickerViewModel()
    {
        _ = LoadAsync();
    }

    private async Task LoadAsync()
    {
        var data = await App.Services.ProductService.GetProductsAsync();
        Products = new ObservableCollection<ProductEntity>(data ?? []);
    }

    [RelayCommand]
    private void Submit()
    {
        RequestClose?.Invoke(SelectedProducts);
    }

    [RelayCommand]
    private void Cancel()
    {
        RequestClose?.Invoke(null);
    }
}