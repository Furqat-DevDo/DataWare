using FluentValidation;

namespace AviaSales.Admin.UseCases.Airport;

public class AirportValidator : AbstractValidator<CreateAirportDto>
{
    public AirportValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(a => a.Code).NotEmpty().NotNull();
        RuleFor(a => a.TZ).NotEmpty().NotNull();
        RuleFor(a => a.TimeZone).NotEmpty().NotNull();
        RuleFor(a => a.City).NotEmpty().NotNull();
        RuleFor(a => a.Country).NotEmpty().NotNull();
        RuleFor(a => a.Label).NotEmpty().NotNull();
        
        RuleFor(a => a.Location).NotNull();
        RuleFor(a => a.Location.Longtitude).NotNull();
        RuleFor(a => a.Location.Latitude).NotNull();
        RuleFor(a => a.Location.Elevation).NotNull();

        RuleFor(a => a.Details).NotNull();
        RuleFor(a => a.Details.IataCode).NotEmpty().NotNull();
        RuleFor(a => a.Details.IcaoCode).NotEmpty().NotNull();
        RuleFor(a => a.Details.Facilities).MaximumLength(400);

        RuleFor(a => a.Type).IsInEnum();
    }
}