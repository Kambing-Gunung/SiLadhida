using CommunityToolkit.Mvvm.ComponentModel;

namespace SiLadhida.App.Features.Product;

public partial class ProductFormModel : ObservableObject
{
    [ObservableProperty]
    private string nama = string.Empty;

    [ObservableProperty]
    private decimal harga;

    [ObservableProperty]
    private int stock;


    [ObservableProperty]
    private string title = "Tambah Produk";

    [ObservableProperty]
    private string errorMessage = "";

    public bool IsEdit { get; set; }

    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

    public bool IsValid()
    {
        return
            !string.IsNullOrWhiteSpace(Nama) &&
            Harga > 0 &&
            Stock >= 0;
    }

    partial void OnErrorMessageChanged(string value)
    {
        OnPropertyChanged(nameof(HasError));
    }

    partial void OnNamaChanged(string value) => Validate();
    partial void OnHargaChanged(decimal value) => Validate();
    partial void OnStockChanged(int value) => Validate();

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Nama))
            ErrorMessage = "Nama wajib diisi";
        else if (Harga <= 0)
            ErrorMessage = "Harga harus lebih dari 0";
        else if (Stock < 0)
            ErrorMessage = "Stock tidak boleh negatif";
        else
            ErrorMessage = "";
    }
}