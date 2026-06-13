using SiLadhida.API.Services.Interfaces;
using SiLadhida.Core.Entities;
using SiLadhida.Core.Enums;
using SiLadhida.Core.Interfaces;

namespace SiLadhida.API.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Order> CreateAsync(
            string namaPemesan)
        {
            var order =
                Order.Create(namaPemesan);

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            _logger.LogInformation(
                "Order created: {OrderId}",
                order.Id);

            return order;
        }

        public async Task<Order?> PayOrderAsync(int orderId)
        {
            var order =
                await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
                return null;

            var productIds = order.Items
                .Select(x => x.ProductId)
                .Distinct()
                .ToList();

            var products =
                await _productRepository.GetByIdsAsync(productIds);

            foreach (var item in order.Items)
            {
                var product = products
                    .FirstOrDefault(
                        p => p.Id == item.ProductId);

                if (product is null)
                    throw new InvalidOperationException(
                        $"Product {item.ProductId} tidak ditemukan.");

                if (product.Stock < item.Quantity)
                    throw new InvalidOperationException(
                        $"Stock {product.Nama} tidak mencukupi.");
            }

            foreach (var item in order.Items)
            {
                var product = products
                    .First(p => p.Id == item.ProductId);

                product.DecreaseStock(item.Quantity);
            }

            order.Pay();

            await _orderRepository.SaveChangesAsync();

            _logger.LogInformation(
                "Order {OrderId} berhasil dibayar",
                order.Id);

            return order;
        }

        public async Task<Order?> UpdateStatusAsync(int id, StateTrigger trigger)
        {
            var order =
                await _orderRepository.GetByIdAsync(id);

            if (order is null)
                return null;

            switch (trigger)
            {
                case StateTrigger.DibatalkanPelanggan:
                    order.Cancel();
                    break;

                case StateTrigger.KueDiambilPelanggan:
                    order.Complete();
                    break;

                default:
                    throw new InvalidOperationException(
                        "Trigger tidak dikenali");
            }

            await _orderRepository.SaveChangesAsync();

            _logger.LogInformation(
                "Order status updated: {OrderId}",
                order.Id);

            return order;
        }

        public async Task<Order?> AddItemAsync(int orderId, int productId, int quantity)
        {
            var order =
                await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
                return null;

            var product =
                await _productRepository.GetByIdAsync(productId);

            if (product is null)
                throw new InvalidOperationException(
                    "Produk tidak ditemukan.");

            order.AddItem(
                product.Id,
                quantity,
                product.Harga);

            await _orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<Order?> RemoveItemAsync(int orderId, int productId)
        {
            var order =
                await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
                return null;

            order.RemoveItem(productId);

            await _orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<Order?> IncreaseItemAsync(int orderId, int productId, int quantity)
        {
            var order =
                await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
                return null;

            order.IncreaseItemQuantity(
                productId,
                quantity);

            await _orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<Order?> DecreaseItemAsync(int orderId, int productId, int quantity)
        {
            var order =
                await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
                return null;

            order.DecreaseItemQuantity(
                productId,
                quantity);

            await _orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<Order?> ClearItemsAsync(int orderId)
        {
            var order =
                await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
                return null;

            order.ClearItems();

            await _orderRepository.SaveChangesAsync();

            return order;
        }
    }
}