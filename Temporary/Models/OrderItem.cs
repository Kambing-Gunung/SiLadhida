namespace SiLadhida.App.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string NamaProduk { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal Harga { get; set; }

        public decimal SubTotal => Quantity * Harga;
    }
}