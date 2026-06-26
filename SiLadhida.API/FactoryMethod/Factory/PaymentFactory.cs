using SiLadhida.API.DTOs;
using SiLadhida.API.FactoryMethod.Product;
using SiLadhida.Core.Entities;

namespace SiLadhida.API.FactoryMethod.Factory;

internal abstract class PaymentFactory
{
    internal abstract IPay CreatePay();
}