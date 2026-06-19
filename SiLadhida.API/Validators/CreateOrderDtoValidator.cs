using FluentValidation;
using SiLadhida.API.DTOs;

namespace SiLadhida.API.Validators;

public class CreateOrderDtoValidator
    : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(x => x.NamaPemesan)
            .NotEmpty()
            .WithMessage("Nama pemesan wajib diisi");

        // RuleFor(x => x.Items)
        //     .NotEmpty()
        //     .WithMessage("Pesanan minimal memiliki 1 item");
    }
}