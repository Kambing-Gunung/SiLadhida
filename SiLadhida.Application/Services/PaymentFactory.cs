using SiLadhida.Application.Interfaces;

namespace SiLadhida.Application.Payments;

public abstract class PaymentFactory
{
    public abstract IPay CreatePay();
}