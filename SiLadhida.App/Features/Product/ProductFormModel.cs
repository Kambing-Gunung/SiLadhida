using CommunityToolkit.Mvvm.ComponentModel;
using System.Globalization;

namespace SiLadhida.App.Features.Product;

public class ProductFormModel : ObservableObject
{
    private string _nama = string.Empty;
    public string Nama
    {
        get => _nama;
        set { if (SetProperty(ref _nama, value)) ClearErrorMessage(); }
    }

    private string _hargaText = string.Empty;
    public string HargaText
    {
        get => _hargaText;
        set { if (SetProperty(ref _hargaText, value)) ClearErrorMessage(); }
    }

    private string _stockText = string.Empty;
    public string StockText
    {
        get => _stockText;
        set { if (SetProperty(ref _stockText, value)) ClearErrorMessage(); }
    }

    private string _title = "Tambah Produk";
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    private string _errorMessage = "";
    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            if (SetProperty(ref _errorMessage, value))
                OnPropertyChanged(nameof(HasError));
        }
    }

    private void ClearErrorMessage()
    {
        if (SetProperty(ref _errorMessage, string.Empty))
            OnPropertyChanged(nameof(HasError));
    }

    private void SetErrorMessage(string message)
    {
        if (SetProperty(ref _errorMessage, message))
            OnPropertyChanged(nameof(HasError));
    }

    public bool IsEdit { get; set; }

    // Hasil parsing — dibaca ProductViewModel
    public decimal Harga { get; set; }
    public int Stock { get; set; }

    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

    public bool IsValid()
    {
        if (string.IsNullOrWhiteSpace(Nama))
        {
            ErrorMessage = "Kolom nama harus diisi";
            return false;
        }

        if (string.IsNullOrWhiteSpace(HargaText))
        {
            ErrorMessage = "Kolom harga harus diisi";
            return false;
        }
        if (!decimal.TryParse(HargaText, NumberStyles.Number, CultureInfo.InvariantCulture, out var harga))
        {
            ErrorMessage = "Harga harus berupa angka";
            return false;
        }
        if (harga <= 0)
        {
            ErrorMessage = "Harga harus lebih dari 0";
            return false;
        }

        if (string.IsNullOrWhiteSpace(StockText))
        {
            ErrorMessage = "Kolom stock harus diisi";
            return false;
        }
        if (!int.TryParse(StockText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var stock))
        {
            ErrorMessage = "Stock harus berupa angka bulat";
            return false;
        }
        if (stock < 0)
        {
            ErrorMessage = "Stock tidak boleh negatif";
            return false;
        }

        Harga = harga;
        Stock = stock;
        ErrorMessage = "";
        return true;
    }
}