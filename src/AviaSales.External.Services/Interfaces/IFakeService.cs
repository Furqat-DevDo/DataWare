namespace AviaSales.External.Services.Interfaces;

public interface IFakeService
{
    Task<bool> BookFlight(long flightId);
    Task<bool> CheckFlight(long flightId);
    Task<bool> UpdateBooking(long bookingId);
    Task<bool> DeleteBooking(long bookingId);
}