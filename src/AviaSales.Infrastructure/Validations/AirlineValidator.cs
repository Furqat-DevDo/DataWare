using AviaSales.Infrastructure.Dtos;
using FluentValidation;

namespace AviaSales.Infrastructure.Validations;

public class AirlineValidator : AbstractValidator<CreateAirlineDto>
{
    public AirlineValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(a => a.Name)
            .NotEmpty()
            .NotNull();
        
        RuleFor(a => a.IataCode)
            .NotEmpty()
            .NotNull()
            .MaximumLength(2)
            .MinimumLength(2);
        
        RuleFor(a => a.IcaoCode)
            .NotEmpty()
            .NotNull()
            .MaximumLength(3)
            .MinimumLength(3);
    }
}