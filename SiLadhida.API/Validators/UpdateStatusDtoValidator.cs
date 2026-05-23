using FluentValidation;
using SiLadhida.API.DTOs;

namespace SiLadhida.API.Validators;

public class UpdateStatusDtoValidator
    : AbstractValidator<UpdateStatusDto>
{
    public UpdateStatusDtoValidator()
    {
        RuleFor(x => x.Trigger)
            .IsInEnum()
            .WithMessage("Trigger status tidak valid");
    }
}