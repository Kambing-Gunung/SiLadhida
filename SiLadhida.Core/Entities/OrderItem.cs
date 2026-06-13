using System;
using SiLadhida.Core.Validators;

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
            OrderItemValidator.ValidateProductId(productId);
            OrderItemValidator.EnsurePositiveQuantity(quantity);
            OrderItemValidator.ValidateHarga(harga);

            ProductId = productId;
            Quantity = quantity;
            Harga = harga;
        }

        public static OrderItem Create(int productId, int quantity, decimal harga)
            => new OrderItem(productId, quantity, harga);

        public void IncreaseQuantity(int quantity)
        {
            OrderItemValidator.EnsurePositiveQuantity(quantity);

            Quantity += quantity;
        }

        public void DecreaseQuantity(int quantity)
        {
            OrderItemValidator.EnsurePositiveQuantity(quantity);
            OrderItemValidator.EnsureAvailableQuantity(Quantity, quantity);

            Quantity -= quantity;
        }
    }
}