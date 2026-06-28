using SiLadhida.API.DTOs;
using SiLadhida.API.FactoryMethod.Product;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.FactoryMethod.Factory;

internal class PaymentOfflineFactory : PaymentFactory
{
    internal override IPay CreatePay() => new CashPayment();
}