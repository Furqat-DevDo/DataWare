using AviaSales.Infrastructure.Dtos;
using FluentValidation;

namespace AviaSales.Infrastructure.Validations;

public class PriceValidator : AbstractValidator<PriceDto>
{
    public PriceValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(p => p.Type).IsInEnum();
        RuleFor(p => p.Amount).GreaterThan(0);
    }
}