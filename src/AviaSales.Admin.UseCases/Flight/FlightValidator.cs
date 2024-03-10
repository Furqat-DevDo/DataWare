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
                => await db.Airlines.AnyAsync(a => a.Id == airlineId, cancellationToken))
            .WithMessage("Airline does not exist.");
        
        RuleFor(f => f.Details).NotNull();
        
        RuleFor(f => f.ArrivalAirportId) 
            .GreaterThan(0)
            .WithMessage("Arrival Airport id must be greater than 0.")
            .MustAsync(async (airportId, cancellationToken)
                => await db.Airports.AnyAsync(a => a.Id == airportId, cancellationToken))
            .WithMessage("Arrval Airport does not exist.");
        
        RuleFor(f => f.DepartureAirportId)
            .GreaterThan(0)
            .WithMessage("Departure airport ID must be greater than 0.")
            .MustAsync(async (airportId, cancellationToken)
                => await db.Airports.AnyAsync(a => a.Id == airportId, cancellationToken))
            .WithMessage("Departure airport does not exist.");

        RuleFor(f => f.ArrivalTime)
            .Must(IsValidDateTime)
            .WithMessage("Arrival time is wrong .");
        
        RuleFor(f => f.DepartureTime)
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

public class UpdateFlightValidator : AbstractValidator<UpdateFlightDto>
{
    public UpdateFlightValidator(AviaSalesDb db)
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        RuleLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(f => f.AirlineId) 
            .GreaterThan(0)
            .WithMessage("Airline id must be grater than 0.")
            .MustAsync(async (airlineId, cancellationToken)
                => await db.Airlines.AnyAsync(a => a.Id == airlineId, cancellationToken))
            .WithMessage("Airline does not exist.");
        
        RuleFor(f => f.ArrivalTime)
            .Must(IsValidDateTime)
            .WithMessage("Arrival time is wrong .");
        
        RuleFor(f => f.DepartureTime)
            .Must(IsValidDateTime)
            .WithMessage("Departue time is wrong .");

        RuleForEach(f => f.Prices)
            .NotNull()
            .SetValidator(new PriceValidator());

        RuleFor(f => f.Details).NotNull();
    }

    private bool IsValidDateTime(DateTime dateTime)
    {
        return dateTime != DateTime.MaxValue && dateTime != DateTime.MinValue;
    }
}