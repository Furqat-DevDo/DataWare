using System.Linq.Expressions;
using AviaSales.Core.Entities;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AviaSales.Admin.UseCases.Flight;

public class FlightManager : BaseManager<AviaSalesDb,Core.Entities.Flight,long,FlightDto>
{
    private readonly ILogger<FlightManager> _logger;
    
    public FlightManager(AviaSalesDb db, ILogger<FlightManager> logger) : base(db)
    {
        _logger = logger;
    }

    protected override Expression<Func<Core.Entities.Flight, FlightDto>> EntityToDto =>
        f => new FlightDto(
            f.Id,
            f.AirlineId,
            f.DepartureAirportId,
            f.DepartureTime,
            f.ArrivalAirportId,
            f.ArrivalTime,
            f.PassengerId,
            f.Prices.Select(p => 
                new PriceDto(p.Amount, p.Type)));
    
    /// <summary>
    /// Will create a new flight.
    /// </summary>
    /// <param name="dto">Create flight dto.</param>
    public async Task<FlightDto> CreateFlight(CreateFlightDto dto)
    {
        var flight = Core.Entities.Flight.Create(
            dto.AirlineId,
            dto.DepartureAirportId,
            dto.DepartureTime,
            dto.ArrivalAirportId,
            dto.ArrivalTime,
            dto.PassengerId);

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
    /// Will update flight information if it is exists else will return null.
    /// </summary>
    /// <param name="id">Flight's id.</param>
    /// <param name="dto">Update Flight dto.</param>
    public async Task<FlightDto?> UpdateFlight(long id, UpdateFlightDto dto)
    {
        var flight = await _db.Flights.FirstOrDefaultAsync(f => f.Id == id);
        if (flight is null) return null;

        flight.Update(dto.AirlineId,dto.DepartureTime,dto.ArrivalTime,dto.PassengerId);

        var prices = dto.Prices.Select(p => new Price {Amount = p.Amount,Type = p.Type});
        flight.UpdatePrices(prices);

        await _db.SaveChangesAsync();
        
        return EntityToDto.Compile().Invoke(flight);
    }
    
    /// <summary>
    /// Will delete flight from db if not exists return false.
    /// </summary>
    /// <param name="id">Flight's id.</param>
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
