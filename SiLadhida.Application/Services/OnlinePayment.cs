using SiLadhida.Application.Interfaces;

namespace SiLadhida.Application.Payments;

public class PaymentOnlineFactory : PaymentFactory
{
    public override IPay CreatePay()
    {
        return new QrisPayment();
    }
}