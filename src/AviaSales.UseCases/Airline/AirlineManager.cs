using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Extensions;
using AviaSales.Shared.Managers;
using AviaSales.Shared.Models;
using Microsoft.EntityFrameworkCore;
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

    /// <summary>
    /// Retrieves a collection of airline data based on the specified filter criteria.
    /// </summary>
    /// <param name="pager">Pagination params.</param>
    /// <param name="filter">An instance of the <see cref="AirlineFilter"/> class containing filter criteria.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation. The result is an enumerable collection of 
    /// <see cref="AirlineDto"/> objects representing the filtered airlines.
    /// </returns>
    public async Task<IEnumerable<AirlineDto>> GetAirLines(Pager pager,AirlineFilter filter)
    {
        var query = _db.Airlines.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(a => a.Name.ToLower().Contains(filter.Name.ToLower()));
        
        if (!string.IsNullOrEmpty(filter.IataCode))
            query = query.Where(a => a.IataCode.ToLower().Contains(filter.IataCode.ToLower()));
        
        if (!string.IsNullOrEmpty(filter.IcaoCode))
            query = query.Where(a => a.IcaoCode.ToLower().Contains(filter.IcaoCode.ToLower()));

        return await query
            .OrderByDescending(a => a.CreatedAt)
            .Paged(pager)
            .Select(EntityToDto)
            .ToListAsync();
    }
}
