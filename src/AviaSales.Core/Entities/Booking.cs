using AviaSales.Core.Enums;
using AviaSales.Shared.Entities;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents a booking entity with information about the associated flight, passenger, and booking details.
/// </summary>
public class Booking : Entity<long>
{
    /// <summary>
    /// Gets or sets the ID of the flight associated with the booking.
    /// </summary>
    public long FlightId { get; private set; }

    /// <summary>
    /// Gets or sets the reference to the associated flight.
    /// </summary>
    public virtual Flight? Flight { get; set; }

    /// <summary>
    /// Gets or sets the ID of the passenger associated with the booking.
    /// </summary>
    public long PassengerId { get; private set; }

    /// <summary>
    /// Gets or sets the reference to the associated passenger.
    /// </summary>
    public virtual Passenger? Passenger { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the booking was made.
    /// </summary>
    public DateTime BookingDateTime { get; private set; }

    /// <summary>
    /// Gets or sets the total price of the booking.
    /// </summary>
    public decimal TotalPrice { get; private set; }

    /// <summary>
    /// Gets or sets the booking status (e.g., confirmed, pending, cancelled).
    /// </summary>
    public EBookingStatus Status { get; private set; }

    /// <summary>
    /// Creates a new booking entity with the specified details.
    /// </summary>
    /// <param name="flightId">The ID of the flight associated with the booking.</param>
    /// <param name="passengerId">The ID of the passenger associated with the booking.</param>
    /// <param name="totalPrice">The total price of the booking.</param>
    /// <param name="status">The booking status (e.g., confirmed, pending, cancelled).</param>
    /// <returns>A new instance of the <see cref="Booking"/> class.</returns>
    public static Booking Create(long flightId, long passengerId,decimal totalPrice, EBookingStatus status)
    {
        var booking = new Booking
        {
            FlightId = flightId,
            PassengerId = passengerId,
            BookingDateTime = DateTime.UtcNow,
            TotalPrice = totalPrice,
            Status = status
        };

        return booking;
    }
    
    /// <summary>
    /// Updates booking price and status.
    /// </summary>
    /// <param name="totalPrice">Booking price.</param>
    /// <param name="status"> Status.</param>
    public void Update(decimal totalPrice, EBookingStatus status)
    {
        TotalPrice = totalPrice;
        Status = status;
    }
}
