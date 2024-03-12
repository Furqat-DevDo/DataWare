using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Extensions;
using AviaSales.Shared.Managers;
using AviaSales.Shared.Models;
using Microsoft.EntityFrameworkCore;
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
            ai.CountryId,
            new AirportDetailsDto(
                ai.Details.IataCode,
                ai.Details.IcaoCode,
                ai.Details.Facilities),
            new LocationDto(
                ai.Location.Longitude,
                ai.Location.Latitude,
                ai.Location.Elevation));
    
    /// <summary>
    /// Retrieves a collection of airports based on the specified filter criteria and pagination settings.
    /// </summary>
    /// <param name="pager">Pagination settings for controlling the result set.</param>
    /// <param name="filter">Filter criteria to refine the search for airports.</param>
    /// <returns>
    /// An asynchronous task that returns a collection of <see cref="AirportDto"/> objects based on the provided filter
    /// and pagination settings.
    /// </returns>
    public async Task<IEnumerable<AirportDto>> GetAirports(Pager pager,AirportFilter filter)
    {
        var query = _db.Airports.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(filter.Code))
            query = query.Where(a => a.Code.ToLower() == filter.Code.ToLower());
        
        if (filter.CountryId.HasValue)
            query = query.Where(a => a.CountryId == filter.CountryId);
        
        if (!string.IsNullOrEmpty(filter.City))
            query = query.Where(a => a.City.ToLower().Contains(filter.City.ToLower()));
        
        if (!string.IsNullOrEmpty(filter.Facilities))
            query = query.Where(a => a.Details.Facilities.ToLower().Contains(filter.Facilities.ToLower()));
        
        if (!string.IsNullOrEmpty(filter.Label))
            query = query.Where(a => a.Details.Facilities.ToLower() == filter.Label.ToLower());
        
        if (!string.IsNullOrEmpty(filter.IataCode))
            query = query.Where(a => a.Details.IataCode.ToLower() == filter.IataCode.ToLower());
        
        if (!string.IsNullOrEmpty(filter.IcaoCode))
            query = query.Where(a => a.Details.IcaoCode.ToLower() == filter.IcaoCode.ToLower());

        return await query
            .OrderByDescending(a => a.CreatedAt)
            .Paged(pager)
            .Select(EntityToDto)
            .ToListAsync();
    }
}
