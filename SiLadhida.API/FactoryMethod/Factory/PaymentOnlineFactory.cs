using SiLadhida.API.DTOs;
using SiLadhida.API.FactoryMethod.Product;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.FactoryMethod.Factory;

internal class PaymentOnlineFactory : PaymentFactory
{
    internal override IPay CreatePay() => new QrisPayment();
}