using System.Collections.Generic;
using SiLadhida.Core.Entities;

namespace SiLadhida.Core.Configs
{
    public static class DataProdukConfig
    {
        public static Dictionary<string, Produk> KodeProduk =
            new Dictionary<string, Produk>()
        {
            { "B1", new Produk { Nama = "Bolu Coklat", Harga = 125000, Stock = 15 } },
            { "B2", new Produk { Nama = "Bolu Strawberry", Harga = 125000, Stock = 15 } },
            { "B3", new Produk { Nama = "Bolu Pandan", Harga = 125000, Stock = 15 } },
            { "B4", new Produk { Nama = "Bolu Keju", Harga = 125000, Stock = 15 } },
            { "B5", new Produk { Nama = "Bolu Coklat Keju", Harga = 130000, Stock = 15 } },
            { "B6", new Produk { Nama = "Bolu Keju Pandan", Harga = 130000, Stock = 15 } },
            { "B7", new Produk { Nama = "Bolu Coklat Strawberry", Harga = 130000, Stock = 15 } },
            { "L1", new Produk { Nama = "Nastar", Harga = 155000, Stock = 20 } },
            { "L2", new Produk { Nama = "Kastengel", Harga = 155000, Stock = 25 } },
            { "L3", new Produk { Nama = "Kue Sagu Keju", Harga = 155000, Stock = 20 } },
            { "L4", new Produk { Nama = "Kue Rambutan Coklat", Harga = 155000, Stock = 20 } }
        };
    }
}