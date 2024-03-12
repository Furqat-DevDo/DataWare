using System.Linq.Expressions;
using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Models;
using AviaSales.Persistence;
using AviaSales.Shared.Extensions;
using AviaSales.Shared.Managers;
using AviaSales.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AviaSales.UseCases.Country;

/// <summary>
/// Manages operations related to the 'Country' entity, serving as an intermediary between the data access layer and higher-level services.
/// </summary>
public class CountryManager : BaseManager<AviaSalesDb, Core.Entities.Country, long, CountryDto>
{
    private readonly ILogger<CountryManager> _logger;
    private readonly ICountryService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="CountryManager"/> class.
    /// </summary>
    /// <param name="db">The database context used for data access.</param>
    /// <param name="service">External country service.</param>
    /// <param name="logger">Logger.</param>
    public CountryManager(AviaSalesDb db, ICountryService service, ILogger<CountryManager> logger) : base(db)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Maps the 'Country' entity to a 'CountryDto' entity.
    /// </summary>
    protected override Expression<Func<Core.Entities.Country, CountryDto>> EntityToDto =>
        c => new CountryDto(
            c.Id,
            c.Name,
            c.Cioc,
            c.Area,
            c.Capital,
            c.Cca2,
            c.Cca3,
            c.Ccn3);

    /// <summary>
    /// Retrieves a list of countries based on the provided filters, combining results from both the database and external services.
    /// </summary>
    /// <param name="filters">Filters to apply when retrieving the list of countries.</param>
    /// <returns>A list of 'CountryDto' entities.</returns>
    public async Task<IEnumerable<CountryDto>> GetList(CountryFilters filters)
    {
        var countries = GetFromDb(filters);
        var externalCountries = GetFromExternal(filters);

        await Task.WhenAll(countries, externalCountries);

        return countries.Result.Concat(externalCountries.Result);
    }

    /// <summary>
    /// Retrieves a list of countries from external services based on the provided filters.
    /// </summary>
    /// <param name="filters">Filters to apply when retrieving the list of countries.</param>
    /// <returns>A list of 'CountryDto' entities.</returns>
    private async Task<IEnumerable<CountryDto>> GetFromExternal(CountryFilters filters)
    {
        var tasks = new List<Task<IEnumerable<RestCountry>>>
        {
            filters.Name is not null ? _service.GetByName(filters.Name)
                : filters.Capital is not null ? _service.GetByCapital(filters.Capital)
                : filters.Code is not null ? _service.GetByCode(filters.Code)
                : _service.GetAll()
        };

        var results = await Task.WhenAll(tasks).ConfigureAwait(false);

        var externals = results.Last().ToList();
        
        if (filters.Page.HasValue || filters.PerPage.HasValue)
        {
            var pager = new Pager(filters.Page, filters.PerPage);
            var paged = externals
                .Skip(pager.Page - 1 * pager.PerPage)
                .Take(pager.PerPage)
                .ToList();
            
            return MapToDto(paged);
        }

           
        return MapToDto(externals);
    }

    /// <summary>
    /// Maps a list of 'RestCountry' entities to a list of 'CountryDto' entities, handling possible null values.
    /// </summary>
    /// <param name="externals">List of 'RestCountry' entities.</param>
    /// <returns>A list of 'CountryDto' entities.</returns>
    private IEnumerable<CountryDto> MapToDto(List<RestCountry> externals)
    {
        if (!externals.Any())
        {
            return Enumerable.Empty<CountryDto>();
        }

        return externals.Select(c => new CountryDto(
            0,
            c.Name?.Official ?? "Unknown",
            c.Cioc ?? "Unknown",
            c.Area ?? 0,
            c.Capital?.FirstOrDefault() ?? "Unknown",
            c.Cca2 ?? "Unknown",
            c.Cca3 ?? "Unknown",
            c.Ccn3 ?? "Unknown"
        ));
    }

    /// <summary>
    /// Retrieves a list of countries from the database based on the provided filters.
    /// </summary>
    /// <param name="filters">Filters to apply when retrieving the list of countries.</param>
    /// <returns>A list of 'CountryDto' entities.</returns>
    private async Task<IEnumerable<CountryDto>> GetFromDb(CountryFilters filters)
    {
        var query = _db.Countries.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(filters.Name))
            query = query.Where(c => c.Name.ToLower().Contains(filters.Name.ToLower()));

        if (!string.IsNullOrEmpty(filters.Capital))
            query = query.Where(c => c.Capital.ToLower().Contains(filters.Capital.ToLower()));

        if (!string.IsNullOrEmpty(filters.Code))
            query = query.Where(c => c.Cca2.ToLower() == filters.Code.ToLower() ||
                                     c.Ccn3.ToLower() == filters.Code.ToLower() ||
                                     c.Cca3.ToLower() == filters.Code.ToLower() ||
                                     c.Cioc.ToLower() == filters.Code.ToLower());

        if (filters.Page.HasValue || filters.PerPage.HasValue)
            query = query.OrderBy(c => c.CreatedAt).Paged(new Pager(filters.Page, filters.PerPage));

        return await query
            .Select(EntityToDto)
            .ToListAsync()
            .ConfigureAwait(false);
    }
}
