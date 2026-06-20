using System;
using SiLadhida.Core.Exceptions;

namespace SiLadhida.Core.Entities
{
    public class OrderItem
    {
        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Harga { get; private set; }
        public decimal SubTotal => Quantity * Harga;

        private OrderItem(int productId, int quantity, decimal harga)
        {
            if (productId <= 0)
                throw new BusinessException("ProductId tidak valid.");

            if (quantity <= 0)
                throw new BusinessException("Quantity harus lebih dari 0.");

            if (harga < 0)
                throw new BusinessException("Harga tidak boleh negatif.");

            ProductId = productId;
            Quantity = quantity;
            Harga = harga;
        }

        public static OrderItem Create(int productId, int quantity, decimal harga)
            => new OrderItem(productId, quantity, harga);

        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new BusinessException("Quantity harus lebih dari 0.");

            Quantity += quantity;

            Quantity += quantity;
        }

        public void DecreaseQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new BusinessException("Quantity harus lebih dari 0.");

            if (Quantity < quantity)
                throw new InvalidOperationException("Quantity tidak mencukupi.");

            Quantity -= quantity;

            Quantity -= quantity;
        }
    }
}