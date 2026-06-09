using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace SiLadhida.App.Models;

public partial class OrderFormModel : ObservableObject
{
    [ObservableProperty]
    private string namaPemesan = string.Empty;

    [ObservableProperty]
    private string statusSekarang = string.Empty;

    [ObservableProperty]
    private decimal totalHarga;

    [ObservableProperty]
    private List<OrderItem> items;

    public bool IsValid()
    {
        return
            !string.IsNullOrWhiteSpace(NamaPemesan)
            && !string.IsNullOrWhiteSpace(StatusSekarang)
            && TotalHarga > 0
            && Items != null && Items.Count > 0;
    }
}