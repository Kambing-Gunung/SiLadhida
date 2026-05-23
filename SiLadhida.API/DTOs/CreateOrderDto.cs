namespace SiLadhida.API.DTOs;

/// <summary>
/// Data transfer object for creating a new order
/// </summary>
public class CreateOrderDto
{
    /// <summary>
    /// The name of the customer placing the order
    /// </summary>
    public string NamaPemesan { get; set; } = string.Empty;

    /// <summary>
    /// The list of items in the order
    /// </summary>
    public List<OrderItemDto> Items { get; set; } = new();
}

/// <summary>
/// Data transfer object for an order item
/// </summary>
public class OrderItemDto
{
    /// <summary>
    /// The product ID for this order item
    /// </summary>
    public int ProdukId { get; set; }

    /// <summary>
    /// The quantity of the product ordered
    /// </summary>
    public int Quantity { get; set; }
}