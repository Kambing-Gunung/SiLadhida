using Avalonia.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Threading.Tasks;

namespace SiLadhida.App.Features.Order;

public partial class CreateOrderDialog : Window, INotifyPropertyChanged
{
    private string _newOrderName = string.Empty;

    public string NewOrderName
    {
        get => _newOrderName;
        set { _newOrderName = value; OnPropertyChanged(); }
    }

    public ObservableCollection<SelectableProductItem> ProductList { get; set; } = new();

    public CreateOrderDialog()
    {
        InitializeComponent();
        
        DataContext = this;
        
        _ = LoadProductsFromDatabaseAsync();
    }

    private async Task LoadProductsFromDatabaseAsync()
    {
        try
        {
            // 🔥 Menggunakan arsitektur service yang baru
            var products = await App.Services.ProductService.GetProductsAsync();
            
            if (products != null)
            {
                foreach (var p in products)
                {
                    ProductList.Add(new SelectableProductItem 
                    { 
                        ProductId = p.Id, 
                        ProductName = p.Nama, 
                        Harga = p.Harga 
                    });
                }
            }
        }
        catch 
        {
            App.Services.Notification.ShowError("Gagal memuat daftar produk.");
        }
    }

    public void CloseDialogCommand()
    {
        this.Close(false);
    }

    public void SubmitOrderCommand()
    {
        if (string.IsNullOrWhiteSpace(NewOrderName))
        {
            App.Services.Notification.ShowError("Nama pemesan wajib diisi!");
            return;
        }

        var checkedItems = ProductList.Where(p => p.IsSelected && p.Quantity > 0).ToList();
        if (checkedItems.Count == 0)
        {
            App.Services.Notification.ShowError("Pilih minimal 1 produk untuk dipesan!");
            return; 
        }

        this.Close(true); 
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class SelectableProductItem : INotifyPropertyChanged
{
    private bool _isSelected;
    private int _quantity = 1;

    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Harga { get; set; }

    public bool IsSelected
    {
        get => _isSelected;
        set { _isSelected = value; OnPropertyChanged(); }
    }

    public int Quantity
    {
        get => _quantity;
        set { _quantity = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}