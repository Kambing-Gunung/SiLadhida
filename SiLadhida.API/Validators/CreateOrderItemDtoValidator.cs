using FluentValidation;
using SiLadhida.API.DTOs;

namespace SiLadhida.API.Validators;

public class CreateOrderItemDtoValidator
    : AbstractValidator<CreateOrderItemDto>
{
    public CreateOrderItemDtoValidator()
    {
        RuleFor(x => x.ProdukId)
            .GreaterThan(0)
            .WithMessage("Produk ID tidak valid");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity minimal 1");
    }
}