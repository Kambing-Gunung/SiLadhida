using System;

namespace SiLadhida.Core.Validators
{
    public static class ProductValidator
    {
        public static void ValidateNama(string nama)
        {
            if (string.IsNullOrWhiteSpace(nama))
                throw new ArgumentException("Nama tidak boleh kosong.");
        }

        public static void ValidateHarga(decimal harga)
        {
            if (harga < 0)
                throw new ArgumentException("Harga tidak boleh negatif.");
        }

        public static void ValidateStock(int stock)
        {
            if (stock < 0)
                throw new ArgumentException("Stock tidak boleh negatif.");
        }

        public static void EnsurePositiveQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity harus lebih dari 0.");
        }
        
        public static void EnsureAvailableStock(int availableStock, int required)
        {
            if (availableStock < required)
            throw new InvalidOperationException("Stock tidak mencukupi.");
        }
    }
}