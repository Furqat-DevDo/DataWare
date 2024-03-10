using Microsoft.EntityFrameworkCore;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents details related to a flight, including passenger count, availability, and baggage information.
/// </summary>
[Owned]
public class FlightDetail
{
    /// <summary>
    /// Gets or sets the count of passengers booked for the flight.
    /// </summary>
    public int PassengerCount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the flight is currently available for booking.
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the flight allows free baggage for passengers.
    /// </summary>
    public bool HasFreeBaggage { get; set; }
}
