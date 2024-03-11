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

    public Task<IEnumerable<FakeFlight>> SearchFlightsAsync(FakeFilters filters)
    {
        throw new NotImplementedException();
    }
}