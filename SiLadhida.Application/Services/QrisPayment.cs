using SiLadhida.Core.Entities;
using SiLadhida.Application.Interfaces;

namespace SiLadhida.Application.Payments;

public class QrisPayment : IPay
{
    public async Task<Order> Payment(int orderId, IOrderService orderService)
    {
        // 🔥 simulasi QRIS (belum langsung konfirmasi)
        // bisa nanti ditambahkan callback / webhook

        await Task.Delay(1000);

        throw new NotImplementedException("QRIS belum diimplementasikan sepenuhnya");
    }
}