using AviaSales.Core.Enums;

namespace AviaSales.Admin.UseCases.Flight;

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
/// Data transfer object for creating a new flight.
/// </summary>
/// <param name="AirlineId">The unique identifier of the associated airline.</param>
/// <param name="DepartureAirportId">The unique identifier of the departure airport.</param>
/// <param name="DepartureTime">The departure time of the flight.</param>
/// <param name="ArrivalAirportId">The unique identifier of the arrival airport.</param>
/// <param name="ArrivalTime">The arrival time of the flight.</param>
/// <param name="Prices">The list of prices associated with the flight.</param>
/// <param name="Details">The flight additional details.</param>
public record CreateFlightDto(long AirlineId, long DepartureAirportId, DateTime DepartureTime, long ArrivalAirportId, 
    DateTime ArrivalTime, FlightDetailDto Details, IEnumerable<PriceDto> Prices);

/// <summary>
/// Data transfer object for representing price information.
/// </summary>
/// <param name="Amount">The amount of the price.</param>
/// <param name="Type">The type of the price.</param>
public record PriceDto(decimal Amount, EPriceType Type);

/// <summary>
/// Data Transfer Object (DTO) for updating flight information, including the airline, departure time, arrival time, 
/// passenger, and associated prices.
/// </summary>
/// <param name="AirlineId">The identifier of the associated airline.</param>
/// <param name="DepartureTime">The updated departure time of the flight.</param>
/// <param name="ArrivalTime">The updated arrival time of the flight.</param>
/// <param name="Prices">A collection of price details associated with the flight.</param>
/// <param name="Details">The additional details about flight.</param>
public record UpdateFlightDto(long AirlineId, DateTime DepartureTime, DateTime ArrivalTime, FlightDetailDto Details, 
    IEnumerable<PriceDto> Prices);

/// <summary>
/// Data Transfer Object (DTO) representing details related to a flight.
/// </summary>
/// <param name="PassengerCount">Max passenger count in flight.</param>
/// <param name="IsAvailable">Is Flight Available now.</param>
/// <param name="HasFreeBagage">Has Flight Free Bagage.</param>
/// <param name="TransactionsCount">Number of transactions.</param>
/// <param name="HasTransaction">Shows is flight direct or not.</param>
public record FlightDetailDto(int PassengerCount,bool IsAvailable,bool HasFreeBagage,int TransactionsCount,bool HasTransaction);

