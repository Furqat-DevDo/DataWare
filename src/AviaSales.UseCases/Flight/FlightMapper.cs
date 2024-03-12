using AviaSales.Core.Enums;
using AviaSales.External.Services.Models;

namespace AviaSales.UseCases.Flight;

public static class FlightMapper
{
    public static FlightDto MapFromMock(FakeFlight mock)
    {
        return new FlightDto(mock.Id,
            mock.ExternalId,
            mock.AirlineId,
            mock.DepartureAirportId,
            mock.DepartureTime,
            mock.ArrivalAirportId,
            mock.ArrivalTime,
            new FlightDetailDto(
                mock.PassengersCount,
                mock.IsAvailable,
                mock.HasFreeBagage,
                mock.Transactions,
                mock.Transactions > 0),
            new PriceDto[] { new (mock.Price,EPriceType.Main)});
    }

    public static FlightDto MapFromExternal(FlightDetails ext)
    {
        // TODO : Parse FlightDetails into FlightDTO !!!!
        throw new NotImplementedException();
    }
}