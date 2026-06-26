using SiLadhida.Core.Entities;
using SiLadhida.Core.Enums;
using SiLadhida.Application.Interfaces;
using SiLadhida.Core.Interfaces;
using SiLadhida.Core.Exceptions;

namespace SiLadhida.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await GetOrderOrThrow(id);
    }

    public async Task<Order> CreateAsync(string namaPemesan)
    {
        var order = Order.Create(namaPemesan);

        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();

        return order;
    }

    public async Task<Order> AddItemAsync(int orderId, int productId, int quantity)
    {
        var order = await GetOrderOrThrow(orderId);
        var product = await GetProductOrThrow(productId);

        var existingItem = order.Items
            .FirstOrDefault(x => x.ProductId == productId);

        var newQuantity = quantity;

        if (existingItem != null)
        {
            newQuantity += existingItem.Quantity;
        }

        EnsureStock(product, newQuantity);

        order.AddItem(product.Id, quantity, product.Harga);

        await _orderRepository.SaveChangesAsync();

        return order;
    }

    public async Task<Order> UpdateStatusAsync(int id, StateTrigger trigger)
    {
        var order = await GetOrderOrThrow(id);

        switch (trigger)
        {
            case StateTrigger.PembayaranDikonfirmasi:
                order.Pay();
                break;

            case StateTrigger.DibatalkanPelanggan:
                order.Cancel();
                break;

            case StateTrigger.KueDiambilPelanggan:
                order.Complete();
                break;

            default:
                throw new BusinessException("Trigger tidak dikenali");
        }

        await _orderRepository.SaveChangesAsync();

        return order;
    }

    public async Task<Order> UpdateItemQuantityAsync(int orderId, int productId, int quantity)
    {
        var order = await GetOrderOrThrow(orderId);

        var item = order.Items.FirstOrDefault(x => x.ProductId == productId)
            ?? throw new BusinessException("Item tidak ditemukan");

        var product = await GetProductOrThrow(productId);

        EnsureStock(product, quantity);

        order.RemoveItem(productId);

        if (quantity > 0)
        {
            order.AddItem(productId, quantity, product.Harga);
        }

        await _orderRepository.SaveChangesAsync();

        return order;
    }

    public async Task<Order> RemoveItemAsync(int orderId, int productId)
    {
        var order = await GetOrderOrThrow(orderId);

        order.RemoveItem(productId);

        await _orderRepository.SaveChangesAsync();

        return order;
    }

    public async Task<Order> ResetItemsAsync(int orderId)
    {
        var order = await GetOrderOrThrow(orderId);

        order.ClearItems();

        await _orderRepository.SaveChangesAsync();

        return order;
    }

    public async Task<Order> PayAsync(int orderId)
    {
        var order = await GetOrderOrThrow(orderId);

        var productIds = order.Items.Select(x => x.ProductId).Distinct().ToList();
        var products = await _productRepository.GetByIdsAsync(productIds);

        foreach (var item in order.Items)
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId)
                ?? throw new NotFoundException($"Product {item.ProductId} tidak ditemukan");

            EnsureStock(product, item.Quantity);
        }

        foreach (var item in order.Items)
        {
            var product = products.First(p => p.Id == item.ProductId);
            product.DecreaseStock(item.Quantity);
        }

        order.Pay();

        await _orderRepository.SaveChangesAsync();

        return order;
    }

    public async Task<Order> CancelAsync(int orderId)
    {
        var order = await GetOrderOrThrow(orderId);

        order.Cancel();

        await _orderRepository.SaveChangesAsync();

        return order;
    }

    public async Task<Order> CompleteAsync(int orderId)
    {
        var order = await GetOrderOrThrow(orderId);

        order.Complete();

        await _orderRepository.SaveChangesAsync();

        return order;
    }

    public async Task DeleteAsync(int id)
    {
        // Mencari data atau mengembalikan error NotFound
        var order = await GetOrderOrThrow(id);
        
        _orderRepository.Delete(order);

        await _orderRepository.SaveChangesAsync();
    }

    // ================= HELPER =================

    private async Task<Order> GetOrderOrThrow(int id)
    {
        return await _orderRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Order tidak ditemukan");
    }

    private async Task<Product> GetProductOrThrow(int id)
    {
        return await _productRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Product tidak ditemukan");
    }

    private void EnsureStock(Product product, int quantity)
    {
        if (product.Stock < quantity)
            throw new BusinessException($"Stock {product.Nama} tidak mencukupi.");
    }
}