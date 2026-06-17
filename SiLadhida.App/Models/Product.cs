namespace SiLadhida.App.Models;

public class Product
{
    public int Id { get; set; }

    public string Nama { get; set; } = string.Empty;

    public decimal Harga { get; set; }

    public int Stock { get; set; }
}