using SiLadhida.API.DTOs;
using SiLadhida.Application.Interfaces;
using SiLadhida.Core.Entities;
using SiLadhida.Core.Enums;

namespace SiLadhida.API.FactoryMethod.Product;

public class QrisPayment : IPay
{
    public Task<Order> Payment(int id, StateTrigger trigger, IOrderService orderService)
    {
        throw new NotImplementedException();
    }
}