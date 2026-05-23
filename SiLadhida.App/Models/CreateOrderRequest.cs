using System.Collections.Generic;

namespace SiLadhida.App.Models
{
    public class CreateOrderRequest
    {
        public string NamaPemesan { get; set; } = string.Empty;

        public List<CreateOrderItem> Items { get; set; } = new();
    }

    public class CreateOrderItem
    {
        public int ProdukId { get; set; }

        public int Quantity { get; set; }
    }
}