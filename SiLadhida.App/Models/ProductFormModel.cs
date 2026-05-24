using CommunityToolkit.Mvvm.ComponentModel;

namespace SiLadhida.App.Models;

public partial class ProductFormModel : ObservableObject
{
    [ObservableProperty]
    private string nama = string.Empty;

    [ObservableProperty]
    private decimal harga;

    [ObservableProperty]
    private int stock;

    public bool IsValid()
    {
        return
            !string.IsNullOrWhiteSpace(Nama)
            && Harga > 0
            && Stock >= 0;
    }
}