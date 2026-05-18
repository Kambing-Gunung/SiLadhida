using System;
using System.Collections.Generic;
using System.Text;

namespace SiLadhida.Core
{
    public class DataProduk
    {
        public static Dictionary<string, Dictionary<string, Produk>> _listProduk = new Dictionary<string, Dictionary<string, Produk>>()
    {
        {
            "Bolu", new Dictionary<string, Produk>()
            {
                { "B1", new Produk("Bolu Coklat", 35000, 10) },
                { "B2", new Produk("Bolu Strawberry", 35000, 10) }
            }
        },
        {
            "Kue Lebaran", new Dictionary<string, Produk>()
            {
                { "L1", new Produk("Nastar", 50000, 20) },
                { "L2", new Produk("Kastengel", 55000, 15) }
            }
        }
    };

        public static Produk AmbilProduk(string jenis, string kode)
        {
            // Parameter input wajib ada 
            if (string.IsNullOrEmpty(jenis) || string.IsNullOrEmpty(kode))
                throw new ArgumentException("Gagal : Jenis dan Kode produk harus diisi.");

            if (_listProduk.ContainsKey(jenis) && _listProduk[jenis].ContainsKey(kode))
            {
                return _listProduk[jenis][kode];
            }

            return null; // Mengembalikan null jika data tidak sesuai tabel
        }
    }
}
