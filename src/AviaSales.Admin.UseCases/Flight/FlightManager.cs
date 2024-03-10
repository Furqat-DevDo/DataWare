using System.Linq.Expressions;
using AviaSales.Core.Entities;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AviaSales.Admin.UseCases.Flight;

/// <summary>
/// Manager class for handling operations related to flights in the AviaSales application.
/// </summary>
public class FlightManager : BaseManager<AviaSalesDb, Core.Entities.Flight, long, FlightDto>
{
    private readonly ILogger<FlightManager> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlightManager"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <param name="logger">The logger for logging messages.</param>
    public FlightManager(AviaSalesDb db, ILogger<FlightManager> logger) : base(db)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets the expression to map Flight entities to FlightDto objects.
    /// </summary>
    protected override Expression<Func<Core.Entities.Flight, FlightDto>> EntityToDto =>
        f => new FlightDto(
            f.Id,
            f.AirlineId,
            f.DepartureAirportId,
            f.DepartureTime,
            f.ArrivalAirportId,
            f.ArrivalTime,
            new FlightDetailDto(
                f.Details.PassengerCount,
                f.Details.IsAvailable,
                f.Details.HasFreeBaggage),
            f.Prices.Select(p => new PriceDto(p.Amount, p.Type)));

    /// <summary>
    /// Creates a new flight.
    /// </summary>
    /// <param name="dto">The data transfer object for creating a flight.</param>
    /// <returns>The created flight represented as a FlightDto.</returns>
    public async Task<FlightDto> CreateFlight(CreateFlightDto dto)
    {
        var flight = Core.Entities.Flight.Create(
            dto.AirlineId,
            dto.DepartureAirportId,
            dto.DepartureTime,
            dto.ArrivalAirportId,
            dto.ArrivalTime,
            new FlightDetail
            {
               IsAvailable = dto.Details.IsAvailable,
               HasFreeBaggage = dto.Details.HasFreeBagage,
               PassengerCount = dto.Details.PassengerCount
            });

        foreach (var price in dto.Prices)
        {
            flight.AddPrice(new Price
            {
                Amount = price.Amount,
                Type = price.Type
            });
        }

        await _db.Flights.AddAsync(flight);
        await _db.SaveChangesAsync();

        return EntityToDto.Compile().Invoke(flight);
    }

    /// <summary>
    /// Updates flight information if it exists; otherwise, returns null.
    /// </summary>
    /// <param name="id">The identifier of the flight to update.</param>
    /// <param name="dto">The data transfer object for updating a flight.</param>
    /// <returns>The updated flight represented as a FlightDto, or null if the flight is not found.</returns>
    public async Task<FlightDto?> UpdateFlight(long id, UpdateFlightDto dto)
    {
        var flight = await _db.Flights.FirstOrDefaultAsync(f => f.Id == id);
        if (flight is null) return null;

        flight.Update(dto.AirlineId, dto.DepartureTime, dto.ArrivalTime,  
        new FlightDetail
        {
            IsAvailable = dto.Details.IsAvailable,
            HasFreeBaggage = dto.Details.HasFreeBagage,
            PassengerCount = dto.Details.PassengerCount
        });

        var prices = dto.Prices.Select(p => new Price { Amount = p.Amount, Type = p.Type });
        flight.UpdatePrices(prices);

        await _db.SaveChangesAsync();

        return EntityToDto.Compile().Invoke(flight);
    }

    /// <summary>
    /// Deletes a flight with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the flight to delete.</param>
    /// <returns>True if the deletion is successful; otherwise, false.</returns>
    public async Task<bool> Delete(long id)
    {
        var flight = await _db.Flights.FirstOrDefaultAsync(f => f.Id == id);

        if (flight is null)
        {
            _logger.LogWarning($"Flight with id {id} not found.");
            return false;
        }

        _db.Flights.Remove(flight);
        return await _db.SaveChangesAsync() > 0;
    }
}
