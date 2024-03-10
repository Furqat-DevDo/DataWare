using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using AviaSales.Shared.Models;
using Microsoft.Extensions.Logging;

namespace AviaSales.UseCases.Flight;

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

    public async Task<IEnumerable<FlightDto>> GetFlights(Pager pager,FlightFilters filters)
    {
        throw new NotImplementedException();
    }
}