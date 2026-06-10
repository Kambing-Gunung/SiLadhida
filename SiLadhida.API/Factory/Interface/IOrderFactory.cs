using System.Collections.Generic;
using System.Threading.Tasks;
using SiLadhida.API.DTOs;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Factories.Interfaces;

/// <summary>
/// Kontrak factory untuk merakit entitas Order yang kompleks
/// </summary>
public interface IOrderFactory
{
    /// <summary>
    /// Membuat entitas Order beserta OrderItem, melakukan validasi stok, 
    /// dan mengkalkulasi total harga.
    /// </summary>
    /// <param name="namaPemesan">Nama pelanggan yang memesan</param>
    /// <param name="itemsDto">Daftar item pesanan dari request</param>
    /// <returns>Entitas Order yang sudah utuh dan valid secara bisnis</returns>
    Task<Order> CreateOrderAsync(string namaPemesan, List<OrderItemDto> itemsDto);
}