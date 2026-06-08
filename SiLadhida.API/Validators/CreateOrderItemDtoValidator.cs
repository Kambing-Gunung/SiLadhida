using FluentValidation;
using SiLadhida.API.DTOs;

namespace SiLadhida.API.Validators;

public class CreateOrderItemDtoValidator
    : AbstractValidator<OrderItemDto>
{
    public CreateOrderItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .WithMessage("Produk ID tidak valid");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity minimal 1");
    }
}