using System.Collections.Generic;

namespace SiLadhida.App.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string NamaPemesan { get; set; } = string.Empty;

        public string StatusSekarang { get; set; } = string.Empty;

        public int TotalHarga { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }
}