namespace SiLadhida.API.DTOs;

public class UpdateProductDto
{
    public string Nama { get; set; } = string.Empty;

    public decimal Harga { get; set; }

    public int Stock { get; set; }
}