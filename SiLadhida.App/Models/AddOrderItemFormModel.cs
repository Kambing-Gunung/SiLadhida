using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace SiLadhida.App.Models;

public partial class AddOrderItemFormModel
    : ObservableObject
{
    public ObservableCollection<Product>
        Products { get; }
        = new();

    [ObservableProperty]
    private Product? selectedProduct;

    [ObservableProperty]
    private int quantity = 1;

    public bool IsValid()
    {
        return
            SelectedProduct != null
            && Quantity > 0;
    }
}