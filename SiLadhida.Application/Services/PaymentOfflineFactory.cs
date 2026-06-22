using SiLadhida.Application.Interfaces;

namespace SiLadhida.Application.Payments;

public class PaymentOfflineFactory : PaymentFactory
{
    public override IPay CreatePay()
    {
        return new CashPayment();
    }
}