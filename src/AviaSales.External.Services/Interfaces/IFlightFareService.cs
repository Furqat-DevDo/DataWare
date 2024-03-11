using AviaSales.External.Services.Models;

namespace AviaSales.External.Services.Interfaces;

public interface IFlightFareService
{
   Task<FlightFare> SearchFlight(SearchData search);
}