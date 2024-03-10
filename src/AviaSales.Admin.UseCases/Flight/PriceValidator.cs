using FluentValidation;

namespace AviaSales.Admin.UseCases.Flight;

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