namespace SiLadhida.API.DTOs.Responses;

public class CreateOrderResponseDto
{
    public int Id { get; set; }

    public string NamaPemesan { get; set; } = string.Empty;

    public int TotalHarga { get; set; }

    public string Status { get; set; } = string.Empty;
}