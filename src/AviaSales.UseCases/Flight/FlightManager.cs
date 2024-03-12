using System.Linq.Expressions;
using AviaSales.Core.Enums;
using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Models;
using AviaSales.Persistence;
using AviaSales.Shared.Extensions;
using AviaSales.Shared.Managers;
using AviaSales.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AviaSales.UseCases.Flight;

/// <summary>
/// Manages operations related to flights in the AviaSales application.
/// </summary>
public class FlightManager : BaseManager<AviaSalesDb, Core.Entities.Flight, long, FlightDto>
{
    private readonly ILogger<FlightManager> _logger;
    private readonly ITimeTableService _timeTableService;
    private readonly IFakeService _fakeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlightManager"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <param name="logger">The logger for logging messages.</param>
    /// <param name="timeTableService">External service for timetable information.</param>
    /// <param name="fakeService">Fake flight service for testing.</param>
    public FlightManager(AviaSalesDb db,
        ILogger<FlightManager> logger,
        ITimeTableService timeTableService,
        IFakeService fakeService) : base(db)
    {
        _logger = logger;
        _timeTableService = timeTableService;
        _fakeService = fakeService;
    }

    /// <summary>
    /// Gets the expression to map Flight entities to FlightDto objects.
    /// </summary>
    protected override Expression<Func<Core.Entities.Flight, FlightDto>> EntityToDto =>
        f => new FlightDto(
            f.Id,
            f.ExternalId,
            f.AirlineId,
            f.DepartureAirportId,
            f.DepartureTime,
            f.ArrivalAirportId,
            f.ArrivalTime,
            new FlightDetailDto(
                f.Details.PassengerCount,
                f.Details.IsAvailable,
                f.Details.HasFreeBaggage,
                f.Details.TransactionsCount,
                f.Details.HasTransaction),
            f.Prices.Select(p => new PriceDto(p.Amount, p.Type)));

    /// <summary>
    /// Gets flights based on provided filters and pager information.
    /// </summary>
    /// <param name="pager">Pager information for pagination.</param>
    /// <param name="filters">Flight filters to apply.</param>
    /// <returns>A collection of FlightDto objects.</returns>
    public async Task<IEnumerable<FlightDto>> GetFlights(Pager pager, FlightFilters filters)
    {
        var result = new List<FlightDto>();

        // Try to get flights from external service.
        var externals = TryGetExternalFLights(filters, pager.PerPage, result);

        // Search flights from the database.
        var flightsInDb = SearchFlightsInDatabaseAsync(filters, pager);

        // Return flights from a mock source.
        var mockFlights = _fakeService.SearchFlightsAsync(new FakeFilters
        {
            DateFrom = filters.DateFrom,
            Transactions = filters.Transactions,
            PriceFrom = filters.PriceFrom,
            PriceTo = filters.PriceTo
        });

        await Task.WhenAll(flightsInDb, mockFlights, externals);

        result.AddRange(flightsInDb.Result);
        result.AddRange(mockFlights.Result.Select(FlightMapper.MapFromMock));

        return result
            .Skip(pager.Page - 1 * pager.PerPage)
            .Take(pager.PerPage);
    }

    /// <summary>
    /// Tries to get flights from an external service and adds them to the result list.
    /// </summary>
    /// <param name="filters">Flight filters.</param>
    /// <param name="perPage">Number of flights per page.</param>
    /// <param name="result">List to store the resulting FlightDto objects.</param>
    private async Task TryGetExternalFLights(FlightFilters filters, byte? perPage, List<FlightDto> result)
    {
        try
        {
            if (!string.IsNullOrEmpty(filters.From)
                && !string.IsNullOrEmpty(filters.To)
                && filters.DateFrom.HasValue)
            {
                var dateString = filters.DateFrom.Value.ToString("yyyyMMdd");
                var timeData = await _timeTableService.SearchFlights(filters.From, filters.To, dateString, perPage
                    ?? 100);

                if (timeData?.FlightDetails is not null)
                {
                    result.AddRange(timeData.FlightDetails.Select(FlightMapper.MapFromExternal));
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "External clients");
        }
    }

    /// <summary>
    /// Searches for flights in the database based on provided filters and pager information.
    /// </summary>
    /// <param name="filters">Flight filters to apply.</param>
    /// <param name="pager">Pager information for pagination.</param>
    /// <returns>A collection of FlightDto objects from the database.</returns>
    private async Task<IEnumerable<FlightDto>> SearchFlightsInDatabaseAsync(FlightFilters filters, Pager pager)
    {
        var query = _db.Flights.AsNoTracking().AsQueryable();

        if (filters.PriceFrom.HasValue)
            query = query.Where(f => f.Prices.First(p => p.Type == EPriceType.Main).Amount >= filters.PriceFrom);

        if (filters.PriceTo.HasValue)
            query = query.Where(f => f.Prices.First(p => p.Type == EPriceType.Main).Amount <= filters.PriceTo);

        if (filters.DateFrom.HasValue)
            query = query.Where(f => f.DepartureTime >= filters.DateFrom);

        if (filters.Transactions.HasValue)
            query = query.Where(f => f.Details.TransactionsCount == filters.Transactions);

        if (!string.IsNullOrEmpty(filters.AirlineIcao))
            query = query.Where(f => f.Airline != null
                && f.Airline.IcaoCode.ToLower() == filters.AirlineIcao.ToLower());

        return await query
            .OrderByDescending(f => f.DepartureTime)
            .Paged(pager)
            .Select(EntityToDto)
            .ToListAsync();
    }
}