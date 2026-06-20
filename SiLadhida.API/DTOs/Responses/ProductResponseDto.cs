namespace SiLadhida.API.DTOs.Responses;

public class ProductResponseDto
{
    public int Id { get; set; }
    public string Nama { get; set; } = string.Empty;
    public decimal Harga { get; set; }
    public int Stock { get; set; }
}