namespace SiLadhida.API.DTOs.Responses;

/// <summary>
/// Data transfer object for order creation response
/// </summary>
public class CreateOrderResponseDto
{
    /// <summary>
    /// The unique identifier of the created order
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the customer who placed the order
    /// </summary>
    public string NamaPemesan { get; set; } = string.Empty;

    /// <summary>
    /// The total price of the order
    /// </summary>
    public int TotalHarga { get; set; }

    /// <summary>
    /// The current status of the order
    /// </summary>
    public string Status { get; set; } = string.Empty;
}