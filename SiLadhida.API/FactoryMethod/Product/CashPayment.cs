using SiLadhida.API.DTOs;
using SiLadhida.Application.Interfaces;
using SiLadhida.Application.Services;
using SiLadhida.Core.Entities;
using SiLadhida.Core.Enums;

namespace SiLadhida.API.FactoryMethod.Product;

public class CashPayment : IPay
{
    public async Task<Order> Payment(int id, StateTrigger trigger, IOrderService orderService)
    {
        var order = await orderService.UpdateStatusAsync(id, trigger);
        return order;
    }
}