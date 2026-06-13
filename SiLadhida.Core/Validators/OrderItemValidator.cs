using System;

namespace SiLadhida.Core.Validators
{
    public static class OrderItemValidator
    {

        public static void ValidateProductId(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("OrderId tidak valid.");
        }

        public static void ValidateHarga(decimal harga)
        {
            if (harga < 0)
                throw new ArgumentException("Harga tidak boleh negatif.");
        }

        public static void EnsurePositiveQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity harus lebih dari 0.");
        }

        public static void EnsureAvailableQuantity(int availableQuantity, int required)
        {
            if (availableQuantity < required)
            throw new InvalidOperationException("Jumlah yang dikurangi melebihi quantity saat ini.");
        }
    }
}