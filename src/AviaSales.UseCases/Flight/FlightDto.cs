using AviaSales.Core.Enums;

namespace AviaSales.UseCases.Flight;

/// <summary>
/// Data transfer object for representing flight information.
/// </summary>
/// <param name="Id">The unique identifier of the flight.</param>
/// <param name="AirlineId">The unique identifier of the associated airline.</param>
/// <param name="DepartureAirportId">The unique identifier of the departure airport.</param>
/// <param name="DepartueTime">The departure time of the flight.</param>
/// <param name="ArrivalAirportId">The unique identifier of the arrival airport.</param>
/// <param name="ArrrivalTime">The arrival time of the flight.</param>
/// <param name="Details">The additional information about flight.</param>
/// <param name="Prices">The list of prices associated with the flight.</param>
public record FlightDto(long Id, long AirlineId, long DepartureAirportId, DateTime DepartueTime, long ArrivalAirportId, 
    DateTime ArrrivalTime, FlightDetailDto Details, IEnumerable<PriceDto> Prices);

/// <summary>
/// Data transfer object for representing price information.
/// </summary>
/// <param name="Amount">The amount of the price.</param>
/// <param name="Type">The type of the price.</param>
public record PriceDto(decimal Amount, EPriceType Type);

/// <summary>
/// Data Transfer Object (DTO) representing details related to a flight.
/// </summary>
/// <param name="PassengerCount">Max passenger count in flight.</param>
/// <param name="IsAvailable">Is Flight Available now.</param>
/// <param name="HasFreeBagage">Has Flight Free Bagage.</param>
public record FlightDetailDto(int PassengerCount,bool IsAvailable,bool HasFreeBagage);

/// <summary>
/// Represents filter criteria for retrieving flight information.
/// </summary>
/// <param name="From">The starting date and time for the flight information.</param>
/// <param name="To">The ending date and time for the flight information.</param>
/// <param name="Airline">The name of the airline for which flight information is filtered.</param>
/// <param name="Transactions">The number of transactions associated with the flight.</param>
public record FlightFilters(DateTime? From,DateTime? To,string? Airline,int? Transactions);
