using System.Linq.Expressions;
using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Models;
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
    private readonly IFlightFareService _flightFareService;
    private readonly IFakeService _fakeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlightManager"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <param name="logger">The logger for logging messages.</param>
    /// <param name="flightFareService">External service.</param>
    /// <param name="fakeService">Fake flight service.</param>
    public FlightManager(AviaSalesDb db, 
        ILogger<FlightManager> logger, 
        IFlightFareService flightFareService, 
        IFakeService fakeService) : base(db)
    {
        _logger = logger;
        _flightFareService = flightFareService;
        _fakeService = fakeService;
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
        var flights = _flightFareService.SearchFlight(new SearchData
        {
            
        });
        
        var databaseTask = SearchFlightsInDatabaseAsync(filters,pager);
        
        var mockFlights = _fakeService.SearchFlightsAsync(
            new FakeFilters
            {
               DateFrom = filters.From,
               DateTo = filters.To,
               Transactions = filters.Transactions,
               PriceFrom = filters.PriceFrom,
               PriceTo = filters.PriceTo,
               Airline = filters.Airline
            });
        
        
        await Task.WhenAll(mockFlights, flights, databaseTask);

        // Aggregate the results
        var flightsFromFare = flights.Result;
        var flightsFromMock = mockFlights.Result;
        var flightsFromDatabase = databaseTask.Result;

        
        // Transform the results to a common flight model
        var result1 = flightsFromFare.Results.Select(f => MapToFareFlightModel(f));
        var result2 =flightsFromMock.Select(f => MapToMockFlightModel(f));
        
        // Combine and deduplicate the results as needed
        var allFlights = flightsFromDatabase;

        return allFlights;
    }
        
    private Task<IEnumerable<FlightDto>> SearchFlightsInDatabaseAsync(FlightFilters fliters,Pager pager)
    {
        throw new NotImplementedException();
    }
    
    // Mapping methods to transform results to a common flight model
    private IEnumerable<FlightDto> MapToFareFlightModel(Result fareFlight)
    {
        // Implement the mapping logic
        // Example: return new CommonFlightModel(fareFlight.Id, fareFlight.Name, ...);
        throw new NotImplementedException();
    }

    private IEnumerable<FlightDto> MapToMockFlightModel(FakeFlight mockFlight)
    {
        // Implement the mapping logic
        // Example: return new CommonFlightModel(mockFlight.Id, mockFlight.Name, ...);
        throw new NotImplementedException();
    }
    
}