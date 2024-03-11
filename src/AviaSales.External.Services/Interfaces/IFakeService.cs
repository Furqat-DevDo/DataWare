using AviaSales.External.Services.Models;

namespace AviaSales.External.Services.Interfaces;

/// <summary>
/// Represents fake internal service with happy path.
/// </summary>
public interface IFakeService
{
    /// <summary>
    /// Should return moore complex object.And must to take more details about flight.
    /// </summary>
    /// <param name="flightId">flight id.</param>
    Task<bool> BookFlight(long flightId);
    
    /// <summary>
    /// Should return moore complex object.And must to take more details about flight.
    /// </summary>
    /// <param name="flightId"></param>
    Task<bool> CheckFlight(long flightId);
    
    /// <summary>
    /// Should return moore complex object.And must to take more details about flight.
    /// </summary>
    /// <param name="bookingId">booking id.</param>
    Task<bool> UpdateBooking(long bookingId);
    
    /// <summary>
    /// Should return moore complex object.And must to take more details about flight.
    /// </summary>
    /// <param name="bookingId">booking id.</param>
    Task<bool> DeleteBooking(long bookingId);
    
    /// <summary>
    /// Will return Fake Flights from mock source.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<FakeFlight>> SearchFlightsAsync(FakeFilters filters);
}