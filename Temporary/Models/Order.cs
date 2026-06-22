using System;
using System.Collections.Generic;
using System.Linq;
using SiLadhida.Core.Enums;

namespace SiLadhida.App.Models;

public class Order
{
    public int Id { get; set; }

    public string NamaPemesan { get; set; } = string.Empty;

    // 🔥 RAW dari API
    public string Status { get; set; } = "";

    // 🔥 MAPPING ke enum
    public StateOrder StatusSekarang =>
        Enum.TryParse<StateOrder>(Status, out var result)
            ? result
            : StateOrder.MenungguPembayaran;

    public decimal TotalHarga => Items.Sum(x => x.SubTotal);

    public bool CanPay => StatusSekarang == StateOrder.MenungguPembayaran;

    public bool CanCancel => StatusSekarang == StateOrder.MenungguPembayaran;

    public bool CanManage => StatusSekarang == StateOrder.MenungguPembayaran;

    public bool CanComplete => StatusSekarang == StateOrder.SiapDiambil;

    public List<OrderItem> Items { get; set; } = new();

    public string StatusClass => StatusSekarang switch
    {
        StateOrder.MenungguPembayaran => "status-pending",
        StateOrder.SiapDiambil => "status-ready",
        StateOrder.Selesai => "status-done",
        StateOrder.Dibatalkan => "status-cancel",
        _ => ""
    };
}