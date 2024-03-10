using AviaSales.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AviaSales.Admin.UseCases.Flight;

public class FlightValidator : AbstractValidator<CreateFlightDto>
{
    public FlightValidator(AviaSalesDb db)
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(f => f.AirlineId) 
            .GreaterThan(0)
            .WithMessage("Airline id must be grater than 0.")
            .MustAsync(async (airlineId, cancellationToken)
                => await db.Airlines.AnyAsync(a => a.Id != airlineId, cancellationToken))
            .WithMessage("Airline does not exist.");
        
        RuleFor(f => f.PassengerId) 
            .GreaterThan(0)
            .WithMessage("Passenger id must be grater than 0.")
            .MustAsync(async (passengerId, cancellationToken)
                => await db.Passengers.AnyAsync(a => a.Id != passengerId, cancellationToken))
            .WithMessage("Passenger does not exist.");
        
        RuleFor(f => f.ArrivalAirportId) 
            .GreaterThan(0)
            .WithMessage("Arrival Airport id must be greater than 0.")
            .MustAsync(async (airportId, cancellationToken)
                => await db.Airports.AnyAsync(a => a.Id != airportId, cancellationToken))
            .WithMessage("Arrval Airport does not exist.");
        
        RuleFor(f => f.DepartureAirportId)
            .GreaterThan(0)
            .WithMessage("Departure airport ID must be greater than 0.")
            .MustAsync(async (airportId, cancellationToken)
                => await db.Airports.AnyAsync(a => a.Id != airportId, cancellationToken))
            .WithMessage("Departure airport does not exist.");

        RuleFor(f => f.ArrrivalTime)
            .Must(IsValidDateTime)
            .WithMessage("Arrival time is wrong .");
        
        RuleFor(f => f.DepartueTime)
            .Must(IsValidDateTime)
            .WithMessage("Departue time is wrong .");

        RuleForEach(f => f.Prices)
            .NotNull()
            .SetValidator(new PriceValidator());
    }
    
    private bool IsValidDateTime(DateTime dateTime)
    {
        return dateTime != DateTime.MaxValue && dateTime != DateTime.MinValue;
    }
}