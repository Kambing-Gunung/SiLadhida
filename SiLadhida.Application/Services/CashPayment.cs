using SiLadhida.Core.Enums;
using SiLadhida.Core.Entities;
using SiLadhida.Application.Interfaces;

namespace SiLadhida.Application.Payments;

public class CashPayment : IPay
{
    public async Task<Order> Payment(int orderId, IOrderService orderService)
    {
        // 🔥 langsung konfirmasi pembayaran
        var order = await orderService.UpdateStatusAsync(
            orderId,
            StateTrigger.PembayaranDikonfirmasi);

        return order;
    }
}