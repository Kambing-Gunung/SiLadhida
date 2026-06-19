using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using SiLadhida.Core.Enums;

namespace SiLadhida.App.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string NamaPemesan { get; set; } = string.Empty;

        public StateOrder StatusSekarang { get; set; }

        public decimal TotalHarga => Items.Sum(x => x.SubTotal);

        public bool CanPay => StatusSekarang == StateOrder.MenungguPembayaran;

        public bool CanCancel => StatusSekarang == StateOrder.MenungguPembayaran;

        public bool CanManage => StatusSekarang == StateOrder.MenungguPembayaran;

        public bool CanComplete => StatusSekarang == StateOrder.SiapDiambil;

        public List<OrderItem> Items { get; set; } = new();

        public void AddItem(Product product)
        {
            var existing = Items.FirstOrDefault(x => x.ProductId == product.Id);

            if (existing != null)
            {
                existing.Quantity++;
                return;
            }

            Items.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = 1,
                Harga = product.Harga
            });
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
}