﻿using AviaSales.Infrastructure.Dtos;
using AviaSales.Infrastructure.Persistance;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AviaSales.Infrastructure.Validations;

public class BookingValidator :AbstractValidator<CreateBookingDto>
{
    public BookingValidator(AviaSalesDb db)
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(b => b.Status).IsInEnum();

        RuleFor(b => b.FlightId)
            .GreaterThan(0)
            .WithMessage("Flight id must be greater than 0.")
            .MustAsync(async (flightId, cancellationToken)
                => await db.Flights.AnyAsync(f => f.Id != flightId, cancellationToken))
            .WithMessage("Flight does not exist.");
        
        RuleFor(b => b.PassengerId)
            .GreaterThan(0)
            .WithMessage("Passenger id must be greater than 0.")
            .MustAsync(async (passengerId, cancellationToken)
                => await db.Passengers.AnyAsync(p => p.Id != passengerId, cancellationToken))
            .WithMessage("Passenger does not exit.");
        
        RuleFor(b => b.TotalPrice).GreaterThan(0);
    }
}