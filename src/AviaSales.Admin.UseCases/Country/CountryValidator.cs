using FluentValidation;

namespace AviaSales.Admin.UseCases.Country;

public class CountryValidator : AbstractValidator<CreateCountryDto>
{
    public CountryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(c => c.Capital).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(c => c.Cioc).NotNull().NotEmpty().MaximumLength(3).MinimumLength(3);
        RuleFor(c => c.Cca3).NotNull().NotEmpty().MaximumLength(3).MinimumLength(3);
        RuleFor(c => c.Cca2).NotNull().NotEmpty().MaximumLength(2).MinimumLength(2);
        RuleFor(c => c.Ccn3).NotNull().NotEmpty().MaximumLength(3).MinimumLength(3);
    }
}