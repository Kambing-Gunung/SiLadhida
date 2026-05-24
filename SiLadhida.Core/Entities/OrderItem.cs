using System.Text.Json.Serialization;

namespace SiLadhida.Core.Entities;

public class OrderItem
{
    public int Id { get; set; }

    public int PesananId { get; set; }

    [JsonIgnore]
    public Pesanan? Pesanan { get; set; }

    public int ProdukId { get; set; }
    
    public Produk? Produk { get; set; }

    public int Quantity { get; set; }

    public int Harga { get; set; }
}