using AviaSales.Shared.Entities;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents a flight entity with information about its airline, departure and arrival airports, schedule, and pricing details.
/// </summary>
public class Flight : Entity<long>
{
    // Private collection for storing prices associated with the flight.
    private readonly HashSet<Price> _prices = new();

    // Private parameterless constructor to prevent direct instantiation.
    private Flight() { }

    /// <summary>
    /// Gets or sets the ID of the airline associated with the flight.
    /// </summary>
    public long AirlineId { get; private set; }

    /// <summary>
    /// Gets or sets the reference to the associated airline entity.
    /// </summary>
    public virtual Airline? Airline { get; private set; }

    /// <summary>
    /// Gets or sets the ID of the departure airport for the flight.
    /// </summary>
    public long DepartureAirportId { get; private set; }

    /// <summary>
    /// Gets or sets the reference to the associated departure airport entity.
    /// </summary>
    public virtual Airport? DepartureAirport { get; private set; }

    /// <summary>
    /// Gets or sets the departure time of the flight.
    /// </summary>
    public DateTime DepartueTime { get; private set; }

    /// <summary>
    /// Gets or sets the ID of the arrival airport for the flight.
    /// </summary>
    public long ArrivalAirportId { get; private set; }

    /// <summary>
    /// Gets or sets the reference to the associated arrival airport entity.
    /// </summary>
    public virtual Airport? ArrivalAirport { get; private set; }

    /// <summary>
    /// Gets or sets the arrival time of the flight.
    /// </summary>
    public DateTime ArrrivalTime { get; private set; }

    /// <summary>
    /// Gets a read-only list of prices associated with the flight.
    /// </summary>
    public IReadOnlyList<Price> Prices => _prices.ToList();

    /// <summary>
    /// Gets a read-only list of bookings associated with the flight.
    /// </summary>
    public IReadOnlyList<Booking> Bookings { get; set; }

    /// <summary>
    /// Foreign key for passenger.
    /// </summary>
    public long PassengerId { get; set; }
    
    /// <summary>
    /// Navigation Property .
    /// </summary>
    public virtual Passenger? Passenger { get; set; }

    /// <summary>
    /// Creates a new flight entity with the specified details.
    /// </summary>
    /// <param name="airlineId">The ID of the airline associated with the flight.</param>
    /// <param name="departureAirportId">The ID of the departure airport for the flight.</param>
    /// <param name="departueTime">The departure time of the flight.</param>
    /// <param name="arrivalAirportId">The ID of the arrival airport for the flight.</param>
    /// <param name="arrrivalTime">The arrival time of the flight.</param>
    /// <param name="passengerId"></param>
    /// <returns>A new instance of the <see cref="Flight"/> class.</returns>
    public static Flight Create(long airlineId, long departureAirportId, DateTime departueTime, long arrivalAirportId, 
        DateTime arrrivalTime,long passengerId)
    {
        var flight = new Flight
        {
            AirlineId = airlineId,
            DepartureAirportId = departureAirportId,
            DepartueTime = departueTime,
            ArrivalAirportId = arrivalAirportId,
            ArrrivalTime = arrrivalTime,
            CreatedAt = DateTime.UtcNow,
            PassengerId = passengerId
        };

        return flight;
    }

    /// <summary>
    /// Adds a price to the flight's collection of prices.
    /// </summary>
    /// <param name="price">The price to be added.</param>
    public void AddPrice(Price price)
    {
        _prices.Add(price);
    }
}
