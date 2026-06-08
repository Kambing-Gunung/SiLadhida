using System.Collections.Generic;
using SiLadhida.Core.Entities;

namespace SiLadhida.Core.Configs
{
    public static class DataProductConfig
    {
        public static Dictionary<string, Product> KodeProduk =
            new Dictionary<string, Product>()
        {
            { "B1", new Product { Nama = "Bolu Coklat", Harga = 125000, Stock = 15 } },
            { "B2", new Product { Nama = "Bolu Strawberry", Harga = 125000, Stock = 15 } },
            { "B3", new Product { Nama = "Bolu Pandan", Harga = 125000, Stock = 15 } },
            { "B4", new Product { Nama = "Bolu Keju", Harga = 125000, Stock = 15 } },
            { "B5", new Product { Nama = "Bolu Coklat Keju", Harga = 130000, Stock = 15 } },
            { "B6", new Product { Nama = "Bolu Keju Pandan", Harga = 130000, Stock = 15 } },
            { "B7", new Product { Nama = "Bolu Coklat Strawberry", Harga = 130000, Stock = 15 } },
            { "L1", new Product { Nama = "Nastar", Harga = 155000, Stock = 20 } },
            { "L2", new Product { Nama = "Kastengel", Harga = 155000, Stock = 25 } },
            { "L3", new Product { Nama = "Kue Sagu Keju", Harga = 155000, Stock = 20 } },
            { "L4", new Product { Nama = "Kue Rambutan Coklat", Harga = 155000, Stock = 20 } }
        };
    }
}