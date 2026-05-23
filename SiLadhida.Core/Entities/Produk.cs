using System;

namespace SiLadhida.Core.Entities
{
    public class Produk
    {
        public int Id { get; set; }
        public string Nama { get; set; } = string.Empty;

        private int _harga;
        public int Harga
        {
            get => _harga;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Harga tidak boleh negatif.");
                _harga = value;
            }
        }

        private int _stock;
        public int Stock
        {
            get => _stock;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Stock tidak boleh kurang dari 0.");
                _stock = value;
            }
        }
    }
}