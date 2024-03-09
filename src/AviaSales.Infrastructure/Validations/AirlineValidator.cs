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
        
        RuleFor(a => a.iataCode)
            .NotEmpty()
            .NotNull()
            .MaximumLength(2);
        
        RuleFor(a => a.icaoCode)
            .NotEmpty()
            .NotNull()
            .MaximumLength(3);
    }
}