using SiLadhida.API.DTOs;
using SiLadhida.API.Factory.Interfaces;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.Factory.Implementations;

public class OrderFactory : IOrderFactory
{
    public Task<Order> CreateAsync(
        string namaPemesan,
        List<OrderItemDto> items)
    {
        var order =
            Order.Create(namaPemesan);

        return Task.FromResult(order);
    }
}