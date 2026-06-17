using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace SiLadhida.App.Models;

public partial class OrderFormModel
    : ObservableObject
{
    [ObservableProperty]
    private string namaPemesan = string.Empty;

    [ObservableProperty]
    private string statusSekarang = string.Empty;

    [ObservableProperty]
    private decimal totalHarga;

    // [ObservableProperty]
    // private ObservableCollection<OrderItem> items
    //     = new();

    public bool IsValid()
    {
        return
            !string.IsNullOrWhiteSpace(namaPemesan);
    }
}