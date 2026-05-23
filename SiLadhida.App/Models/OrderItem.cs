namespace SiLadhida.App.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int ProdukId { get; set; }

        public int Quantity { get; set; }

        public int Harga { get; set; }
    }
}