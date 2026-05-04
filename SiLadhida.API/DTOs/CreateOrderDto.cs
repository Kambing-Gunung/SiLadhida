namespace SiLadhida.API.DTOs
{
    public class CreateOrderDto
    {
        public string NamaPemesan { get; set; } = string.Empty;
        public List<OrderItemDto> Items { get; set; } = new();
    }

    public class OrderItemDto
    {
        public int ProdukId { get; set; }
        public int Quantity { get; set; }
    }
}