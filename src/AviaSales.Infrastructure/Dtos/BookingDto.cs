using AviaSales.Core.Enums;

namespace AviaSales.Infrastructure.Dtos;

/// <summary>
/// Data transfer object for representing booking information.
/// </summary>
/// <param name="Id">The unique identifier of the booking.</param>
/// <param name="FlightId">The unique identifier of the associated flight.</param>
/// <param name="PassengerId">The unique identifier of the associated passenger.</param>
/// <param name="TotalPrice">The total price of the booking.</param>
/// <param name="Status">The status of the booking.</param>
/// <param name="CreatedAt">The date and time when the booking was created.</param>
/// <param name="UpdatedAt">The date and time when the booking was last updated (nullable).</param>
public record BookingDto(long Id, long FlightId, long PassengerId, decimal TotalPrice, EBookingStatus Status,
    DateTime CreatedAt, DateTime? UpdatedAt);

/// <summary>
/// Data transfer object for creating a new booking.
/// </summary>
/// <param name="FlightId">The unique identifier of the associated flight.</param>
/// <param name="PassengerId">The unique identifier of the associated passenger.</param>
/// <param name="TotalPrice">The total price of the booking.</param>
/// <param name="Status">The status of the booking.</param>
public record CreateBookingDto(long FlightId, long PassengerId, decimal TotalPrice, EBookingStatus Status);

/// <summary>
/// Data transfer object for updating an existing booking.
/// </summary>
/// <param name="TotalPrice">The updated total price of the booking.</param>
/// <param name="Status">The updated status of the booking.</param>
public record UpdateBookingDto(decimal TotalPrice, EBookingStatus Status);
