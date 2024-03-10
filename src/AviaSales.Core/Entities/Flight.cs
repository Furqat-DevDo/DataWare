using AviaSales.Shared.Entities;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents a flight entity with information about its airline, departure and arrival airports, schedule, and pricing details.
/// </summary>
public class Flight : Entity<long>
{
    // Private collection for storing prices associated with the flight.
    private readonly List<Price> _prices = new();

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
    public DateTime DepartureTime { get; private set; }

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
    public DateTime ArrivalTime { get; private set; }

    /// <summary>
    /// Gets a read-only list of prices associated with the flight.
    /// </summary>
    public IReadOnlyList<Price> Prices => _prices;

    /// <summary>
    /// Gets a read-only list of bookings associated with the flight.
    /// </summary>
    public IReadOnlyList<Booking> Bookings { get; set; }
    
    /// <summary>
    /// Additional informations about flight.
    /// </summary>
    public FlightDetail Details { get; private set; }

    /// <summary>
    /// Creates a new flight entity with the specified details.
    /// </summary>
    /// <param name="airlineId">The ID of the airline associated with the flight.</param>
    /// <param name="departureAirportId">The ID of the departure airport for the flight.</param>
    /// <param name="departueTime">The departure time of the flight.</param>
    /// <param name="arrivalAirportId">The ID of the arrival airport for the flight.</param>
    /// <param name="arrrivalTime">The arrival time of the flight.</param>
    /// <param name="details">Additional information about Flight.</param>
    /// <returns>A new instance of the <see cref="Flight"/> class.</returns>
    public static Flight Create(long airlineId, long departureAirportId, DateTime departueTime, long arrivalAirportId, 
        DateTime arrrivalTime,FlightDetail details)
    {
        var flight = new Flight
        {
            AirlineId = airlineId,
            DepartureAirportId = departureAirportId,
            DepartureTime = departueTime,
            ArrivalAirportId = arrivalAirportId,
            ArrivalTime = arrrivalTime,
            CreatedAt = DateTime.UtcNow,
            Details = details
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

    /// <summary>
    /// Updates flight information, including the airline, departure time, arrival time, and passenger.
    /// </summary>
    /// <param name="airlineId">The identifier of the associated airline.</param>
    /// <param name="departureTime">The updated departure time of the flight.</param>
    /// <param name="arrivalTime">The updated arrival time of the flight.</param>
    /// <param name="details">Additional informations about Flight.</param>
    public void Update(long airlineId, DateTime departureTime, DateTime arrivalTime,FlightDetail details)
    {
        AirlineId = airlineId;
        DepartureTime = departureTime;
        ArrivalTime = arrivalTime;
        CreatedAt = DateTime.UtcNow;
        Details = details;
    }

    /// <summary>
    /// Updates the prices associated with the flight.
    /// </summary>
    /// <param name="prices">A collection of Price objects representing the updated prices for the flight.</param>
    public void UpdatePrices(IEnumerable<Price> prices)
    {
        // Clears existing prices and adds the updated prices to the flight.
        _prices.Clear();
        _prices.AddRange(prices);
    }
}
