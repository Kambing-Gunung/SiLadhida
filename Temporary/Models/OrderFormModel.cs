using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace SiLadhida.App.Models;

public partial class OrderFormModel : ObservableObject
{
    [ObservableProperty]
    private string namaPemesan = string.Empty;

    [ObservableProperty]
    private string statusSekarang = string.Empty;

    public ObservableCollection<OrderItemFormModel> Items { get; } = new();

    public decimal TotalHarga => Items.Sum(x => x.SubTotal);

    public void RefreshTotal()
    {
        OnPropertyChanged(nameof(TotalHarga));
    }

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(NamaPemesan);
    }
}