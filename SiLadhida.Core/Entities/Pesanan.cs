using SiLadhida.Core.Enums;

namespace SiLadhida.Core.Entities
{
    public class Pesanan
    {
        public int Id { get; set; }
        public string NamaPemesan { get; set; } = string.Empty;
        public string NamaKue { get; set; } = string.Empty;
        public Status StatusSekarang { get; set; }

        public List<OrderItem> Items { get; set; } = new();
        public int TotalHarga { get; set; }
    }
}
