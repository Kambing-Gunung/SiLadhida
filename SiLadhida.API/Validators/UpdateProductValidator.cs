using FluentValidation;
using SiLadhida.API.DTOs;

namespace SiLadhida.API.Validators;

public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Nama)
            .NotEmpty();

        RuleFor(x => x.Harga)
            .GreaterThan(0);

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0);
    }
}