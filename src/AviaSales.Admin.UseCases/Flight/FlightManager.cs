using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
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
            f.DepartueTime,
            f.ArrivalAirportId,
            f.ArrrivalTime,
            f.PassengerId,
            f.Prices.Select(p => 
                new PriceDto(p.Amount, p.Type)));

    public async Task<object?> CreateFlight(CreateFlightDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<object?> UpdateFlight(long id, CreateFlightDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<object?> Delete(long id)
    {
        throw new NotImplementedException();
    }
}