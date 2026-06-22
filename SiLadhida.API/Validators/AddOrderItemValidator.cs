using FluentValidation;
using SiLadhida.API.DTOs;

namespace SiLadhida.API.Validators;

public class AddOrderItemValidator : AbstractValidator<AddOrderItemDto>
{
    public AddOrderItemValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}