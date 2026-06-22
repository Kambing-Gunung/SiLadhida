using CommunityToolkit.Mvvm.ComponentModel;
using SiLadhida.Core.Enums;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SiLadhida.App.Features.Order;

public partial class Order : ObservableObject
{
    public int Id { get; set; }

    public string NamaPemesan { get; set; } = string.Empty;

    public string Status { get; set; } = "";

    public string StatusText => StatusSekarang.ToString();
    
    public StateOrder StatusSekarang =>
        Enum.TryParse<StateOrder>(Status, true, out var result)
            ? result
            : StateOrder.MenungguPembayaran;

    [ObservableProperty]
    private ObservableCollection<OrderItem> items = new();

    public decimal TotalHarga => Items.Sum(x => x.SubTotal);

    public bool CanPay => StatusSekarang == StateOrder.MenungguPembayaran;
    public bool CanCancel => StatusSekarang == StateOrder.MenungguPembayaran;
    public bool CanManage => StatusSekarang == StateOrder.MenungguPembayaran;
    public bool CanComplete => StatusSekarang == StateOrder.SiapDiambil;


    partial void OnItemsChanged(ObservableCollection<OrderItem> value)
    {
        OnPropertyChanged(nameof(TotalHarga));
    }

    public void RefreshTotals()
    {
        OnPropertyChanged(nameof(TotalHarga));
    }

    public string StatusClass => StatusSekarang switch
    {
        StateOrder.MenungguPembayaran => "status-pending",
        StateOrder.SiapDiambil => "status-ready",
        StateOrder.Selesai => "status-done",
        StateOrder.Dibatalkan => "status-cancel",
        _ => ""
    };
}