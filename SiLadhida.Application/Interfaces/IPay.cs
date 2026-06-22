using SiLadhida.Core.Enums;
using SiLadhida.Core.Entities;
using SiLadhida.Application.Interfaces;

namespace SiLadhida.Application.Interfaces;

public interface IPay
{
    Task<Order> Payment(int orderId, IOrderService orderService);
}