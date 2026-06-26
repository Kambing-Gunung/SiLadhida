using CommunityToolkit.Mvvm.ComponentModel;

namespace SiLadhida.App.Features.Order;

public partial class OrderItem : ObservableObject
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    [ObservableProperty]
    public string namaProduk = string.Empty;

    [ObservableProperty]
    private int quantity = 1; // Default 1

    public decimal Harga { get; set; }
    
    // Tambahan untuk validasi
    public int StockTerbaru { get; set; } 

    public decimal SubTotal => Quantity * Harga;

    partial void OnQuantityChanged(int value)
    {
        OnPropertyChanged(nameof(SubTotal));
    }
}