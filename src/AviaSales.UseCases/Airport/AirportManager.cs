using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using Microsoft.Extensions.Logging;

namespace AviaSales.UseCases.Airport;

/// <summary>
/// Manager class for handling operations related to airports in the AviaSales application.
/// </summary>
public class AirportManager : BaseManager<AviaSalesDb, Core.Entities.Airport, long, AirportDto>
{
    private readonly ILogger<AirportManager> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AirportManager"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <param name="logger">The logger for logging messages.</param>
    public AirportManager(AviaSalesDb db, ILogger<AirportManager> logger) : base(db)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets the expression to map Airport entities to AirportDto objects.
    /// </summary>
    protected override Expression<Func<Core.Entities.Airport, AirportDto>> EntityToDto =>
        ai => new AirportDto(
            ai.Id,
            ai.Code,
            ai.TZ,
            ai.TimeZone,
            ai.Type,
            ai.Label,
            ai.City,
            ai.Country,
            new AirportDetailsDto(
                ai.Details.IataCode,
                ai.Details.IcaoCode,
                ai.Details.Facilities),
            new LocationDto(
                ai.Location.Longitude,
                ai.Location.Latitude,
                ai.Location.Elevation));
}
