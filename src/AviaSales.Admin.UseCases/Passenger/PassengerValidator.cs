using AviaSales.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AviaSales.Admin.UseCases.Passenger;

public class PassengerValidator : AbstractValidator<CreatePassengerDto>
{
    public PassengerValidator(AviaSalesDb db)
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(p => p.FlightId)
            .GreaterThan(0)
            .WithMessage("Flight ID must be greater than 0.")
            .MustAsync(async (flightId, cancellationToken)
                => await db.Airports.AnyAsync(a => a.Id != flightId, cancellationToken))
            .WithMessage("Flight does not exist.");

        RuleFor(p => p.Fullname).NotNull().NotEmpty();
        RuleFor(p => p.Phone).NotNull().NotEmpty();
        RuleFor(p => p.Email).NotNull().NotEmpty();
    }
}