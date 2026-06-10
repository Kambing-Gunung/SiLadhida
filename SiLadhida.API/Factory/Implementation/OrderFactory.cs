using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiLadhida.API.Data;
using SiLadhida.API.DTOs;
using SiLadhida.API.Factories.Interfaces;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Factories.Implementations;

/// <summary>
/// Implementasi factory untuk merakit entitas Order
/// </summary>
public class OrderFactory : IOrderFactory
{
    private readonly AppDbContext _context;
    private readonly Core.Services.OrderService _coreOrderService;

    public OrderFactory(
        AppDbContext context, 
        Core.Services.OrderService coreOrderService)
    {
        _context = context;
        _coreOrderService = coreOrderService;
    }

    public async Task<Order> CreateOrderAsync(string namaPemesan, List<OrderItemDto> itemsDto)
    {
        // 1. Validasi input dasar
        if (string.IsNullOrWhiteSpace(namaPemesan))
            throw new ArgumentNullException(nameof(namaPemesan), "Nama pemesan tidak boleh kosong");

        if (itemsDto == null || itemsDto.Count == 0)
            throw new ArgumentException("Pesanan minimal memiliki 1 item", nameof(itemsDto));

        // 2. Inisialisasi entitas Order dengan status awal
        var order = new Order
        {
            NamaPemesan = namaPemesan,
            StatusSekarang = _coreOrderService.GetInitialStatus(),
            Items = new List<OrderItem>()
        };

        // 3. Proses perakitan item dan validasi bisnis (Stok dll)
        foreach (var itemDto in itemsDto)
        {
            // Ambil data produk dari database untuk memastikan produk ada dan harga valid
            var product = await _context.Product.FindAsync(itemDto.ProductId);

            if (product == null)
            {
                throw new InvalidOperationException($"Produk dengan ID {itemDto.ProductId} tidak ditemukan di sistem.");
            }

            if (itemDto.Quantity > product.Stock)
            {
                throw new InvalidOperationException(
                    $"Stock produk '{product.Nama}' tidak mencukupi. Tersedia: {product.Stock}, Diminta: {itemDto.Quantity}");
            }

            // Buat entitas OrderItem
            var item = new OrderItem
            {
                ProductId = product.Id,
                Quantity = itemDto.Quantity,
                Harga = product.Harga, // Harga dikunci berdasarkan harga saat ini di database
                Order = order          // Kaitkan kembali ke parent Order
            };

            // Kurangi stok di memori (EF Core akan menyimpannya saat SaveChanges dipanggil nanti)
            product.Stock -= itemDto.Quantity;
            
            // Masukkan item ke dalam keranjang pesanan
            order.Items.Add(item);
        }

        // 4. Kalkulasi total harga berdasarkan item yang sudah dirakit
        order.TotalHarga = _coreOrderService.HitungTotal(order.Items);

        // 5. Kembalikan entitas yang sudah siap disimpan
        return order;
    }
}