using SiLadhida.API.DTOs;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Factory.Interfaces;

public interface IOrderFactory
{
    Task<Order> CreateAsync(
        string namaPemesan,
        List<OrderItemDto> items);
}