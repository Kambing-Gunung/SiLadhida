using System.Collections.Generic;
using SiLadhida.Core.Entities;

namespace SiLadhida.Core.Configs
{
    public static class DataProdukConfig
    {
        public static Dictionary<string, Produk> KodeProduk =
            new Dictionary<string, Produk>()
        {
            { "B1", new Produk { Nama = "Bolu Coklat", Harga = 10000, Stock = 10 } },
            { "B2", new Produk { Nama = "Bolu Strawberry", Harga = 12000, Stock = 15 } },
            { "B3", new Produk { Nama = "Bolu Pandan", Harga = 12000, Stock = 15 } },
            { "B4", new Produk { Nama = "Bolu Keju", Harga = 12000, Stock = 15 } },
            { "B5", new Produk { Nama = "Bolu Coklat Keju", Harga = 12000, Stock = 15 } },
            { "B6", new Produk { Nama = "Bolu Keju Pandan", Harga = 12000, Stock = 15 } },
            { "B7", new Produk { Nama = "Bolu Coklat Strawberry", Harga = 12000, Stock = 15 } },
            { "L1", new Produk { Nama = "Nastar", Harga = 8000, Stock = 20 } },
            { "L2", new Produk { Nama = "Kastengel", Harga = 9000, Stock = 25 } },
            { "L3", new Produk { Nama = "Kue Sagu Keju", Harga = 8000, Stock = 20 } },
            { "L4", new Produk { Nama = "Kue Rambutan Coklat", Harga = 8000, Stock = 20 } }
        };
    }
}