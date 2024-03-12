using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Models;

namespace AviaSales.External.Services.Services;

/// <summary>
/// Fake Service represent external service only with happy path.
/// </summary>
public class FakeService : IFakeService
{
    public Task<bool> BookFlight(long flightId)
        => Task.FromResult(true);

    public Task<bool> CheckFlight(long flightId)
        => Task.FromResult(true);

    public Task<bool> UpdateBooking(long bookingId)
        => Task.FromResult(true);

    public Task<bool> DeleteBooking(long bookingId)
        => Task.FromResult(true);

    /// <summary>
    /// Will return fake flight lists from fake source.
    /// </summary>
    /// <param name="filters">Some filters.</param>
    public Task<IEnumerable<FakeFlight>> SearchFlightsAsync(FakeFilters filters)
    {
        var flights = FakeFlight.GenerateFakeFlights(250);
        
        if (filters.PriceFrom.HasValue)
            flights = flights.Where(f => f.Price >= filters.PriceFrom);
        
        if (filters.PriceTo.HasValue)
            flights = flights.Where(f => f.Price <= filters.PriceTo);
        
        if (filters.Transactions.HasValue)
            flights = flights.Where(f => f.Transactions == filters.Transactions);
        
        if (filters.DateFrom.HasValue)
            flights = flights.Where(f => f.DepartureTime >= filters.DateFrom);
        
        return Task.FromResult(flights);
    }
        
}