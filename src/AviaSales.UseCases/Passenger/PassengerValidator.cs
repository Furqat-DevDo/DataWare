using AviaSales.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AviaSales.UseCases.Passenger;

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
                => await db.Airports.AnyAsync(a => a.Id == flightId, cancellationToken))
            .WithMessage("Flight does not exist.");

        RuleFor(p => p.Fullname).NotNull().NotEmpty();
        
        RuleFor(p => p.Phone).NotNull().NotEmpty()
            .Matches(@"^(\+\d{1,2}\s?)?(\(\d{1,4}\)|\d{1,4})[-.\s]?\d{1,10}$")
            .WithMessage("Invalid phone number format. Please enter a valid phone number.");
        
        RuleFor(p => p.Email).NotNull().NotEmpty().EmailAddress();
    }
}