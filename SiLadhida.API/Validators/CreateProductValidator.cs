using FluentValidation;
using SiLadhida.API.DTOs;

namespace SiLadhida.API.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Nama)
            .NotEmpty().WithMessage("Nama produk wajib diisi")
            .MaximumLength(100);

        RuleFor(x => x.Harga)
            .GreaterThan(0).WithMessage("Harga harus lebih dari 0");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock tidak boleh negatif");
    }
}