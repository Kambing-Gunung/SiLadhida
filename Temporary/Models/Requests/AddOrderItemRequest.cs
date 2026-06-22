namespace SiLadhida.App.Models.Requests;

public class AddOrderItemRequest
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}