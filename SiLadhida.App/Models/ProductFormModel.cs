using CommunityToolkit.Mvvm.ComponentModel;

namespace SiLadhida.App.Models;

public partial class ProductFormModel : ObservableObject
{
    [ObservableProperty]
    private Product? selectedProduct;

    [ObservableProperty]
    private string nama = string.Empty;

    [ObservableProperty]
    private decimal harga;

    [ObservableProperty]
    private int stock;

    [ObservableProperty]
    private int quantity = 1;

    public bool IsValid()
    {
        return
            selectedProduct != null
            && quantity > 0;
    }
}