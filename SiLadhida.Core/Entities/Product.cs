using System;
using SiLadhida.Core.Validators;

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
            ProductValidator.ValidateNama(nama);
            ProductValidator.ValidateHarga(harga);
            ProductValidator.ValidateStock(stock);

            Nama = nama.Trim();
            Harga = harga;
            Stock = stock;
        }

        public static Product Create(string nama, decimal harga, int stock)
            => new Product(nama, harga, stock);

        public void Rename(string namaBaru)
        {
            ProductValidator.ValidateNama(namaBaru);
            Nama = namaBaru.Trim();
        }

        public void UpdatePrice(decimal hargaBaru)
        {
            ProductValidator.ValidateHarga(hargaBaru);
            Harga = hargaBaru;
        }

        public void SetStock(int stockBaru)
        {
            ProductValidator.ValidateStock(stockBaru);
            Stock = stockBaru;
        }

        public void IncreaseStock(int quantity)
        {
            ProductValidator.EnsurePositiveQuantity(quantity);
            Stock += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            ProductValidator.EnsureAvailableStock(Stock, quantity);

            Stock -= quantity;
        }
    }
}