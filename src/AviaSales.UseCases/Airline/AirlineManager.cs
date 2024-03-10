using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using Microsoft.Extensions.Logging;

namespace AviaSales.UseCases.Airline;

/// <summary>
/// Manager class for handling operations related to airlines in the AviaSales application.
/// </summary>
public class AirlineManager : BaseManager<AviaSalesDb, Core.Entities.Airline, long, AirlineDto>
{
    private readonly ILogger<AirlineManager> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AirlineManager"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <param name="logger">The logger for logging messages.</param>
    public AirlineManager(AviaSalesDb db, ILogger<AirlineManager> logger) : base(db)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets the expression to map Airline entities to AirlineDto objects.
    /// </summary>
    protected override Expression<Func<Core.Entities.Airline, AirlineDto>> EntityToDto =>
        a => new AirlineDto(
            a.Id,
            a.CreatedAt,
            a.IataCode,
            a.IcaoCode,
            a.Name);
}
