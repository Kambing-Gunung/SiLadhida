using System.Collections.Generic;
namespace SiLadhida.App.Shared.Requests;

public class CreateOrderRequest
{
    public string NamaPemesan { get; set; }
        = string.Empty;
        public List<OrderItemRequest> Items { get; set; } = new();
}
public class OrderItemRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}