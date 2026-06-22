namespace SiLadhida.API.DTOs.Responses;

public class OrderResponseDto
{
    public int Id { get; set; }
    public string NamaPemesan { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal TotalHarga { get; set; }

    public List<OrderItemResponseDto> Items { get; set; } = new();
}

public class OrderItemResponseDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Harga { get; set; }
    public decimal SubTotal { get; set; }
}