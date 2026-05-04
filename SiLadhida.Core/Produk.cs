using System;
using System.Collections.Generic;
using System.Text;

namespace SiLadhida.Core
{
    public class Produk
    {
        private int _harga;
        private int _stock;

        public string Nama { get; set; }

        public int Harga
        {
            get => _harga;
            set
            {
                // Harga tidak boleh negatif
                if (value < 0) throw new ArgumentException("Harga tidak boleh negatif.");
                _harga = value;
            }
        }

        public int Stock
        {
            get => _stock;
            set
            {
                // Stock tidak boleh negatif
                if (value < 0) throw new ArgumentException("Stock tidak boleh kurang dari 0.");
                _stock = value;
            }
        }

        public Produk(string nama, int harga, int stock)
        {
            this.Nama = nama;
            this.Harga = harga;
            this.Stock = stock;
        }
    }
}
