using CommunityToolkit.Mvvm.ComponentModel;

namespace SiLadhida.App.Features.Product;

public partial class Product : ObservableObject
{
    public int Id { get; set; }

    [ObservableProperty]
    private string nama = string.Empty;

    [ObservableProperty]
    private decimal harga;

    [ObservableProperty]
    private int stock;

    [ObservableProperty]
    private bool isSelected;
}