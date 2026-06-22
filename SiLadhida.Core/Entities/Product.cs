using System;

namespace SiLadhida.Core.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Nama { get; private set; }
        public decimal Harga { get; private set; }
        public int Stock { get; private set; }

        private Product(string nama, decimal harga, int stock)
        {
            if (string.IsNullOrWhiteSpace(nama))
                throw new ArgumentException("Nama tidak boleh kosong.");

            if (harga < 0)
                throw new ArgumentException("Harga tidak boleh negatif.");

            if (stock < 0)
                throw new ArgumentException("Stock tidak boleh negatif.");

            Nama = nama.Trim();
            Harga = harga;
            Stock = stock;
        }

        public static Product Create(string nama, decimal harga, int stock)
            => new Product(nama, harga, stock);

        public void Rename(string namaBaru)
        {
            if (string.IsNullOrWhiteSpace(namaBaru))
                throw new ArgumentException("Nama tidak boleh kosong.");

            Nama = namaBaru.Trim();
        }

        public void UpdatePrice(decimal hargaBaru)
        {
            if (hargaBaru < 0)
                throw new ArgumentException("Harga tidak boleh negatif.");
            Harga = hargaBaru;
        }

        public void SetStock(int stockBaru)
        {
            if (stockBaru < 0)
                throw new ArgumentException("Stock tidak boleh negatif.");
            Stock = stockBaru;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity harus lebih dari 0.");

            Stock += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            if (Stock < quantity)
                throw new InvalidOperationException("Stock tidak mencukupi.");

            Stock -= quantity;
        }
    }
}