using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AviaSales.UseCases.Booking;

/// <summary>
/// Manager class for handling operations related to bookings in the AviaSales application.
/// </summary>
public class BookingManager : BaseManager<AviaSalesDb, Core.Entities.Booking, long, BookingDto>
{
    private readonly ILogger<BookingManager> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingManager"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <param name="logger">The logger for logging messages.</param>
    public BookingManager(AviaSalesDb db, ILogger<BookingManager> logger) : base(db)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets the expression to map Booking entities to BookingDto objects.
    /// </summary>
    protected override Expression<Func<Core.Entities.Booking, BookingDto>> EntityToDto =>
        b => new BookingDto(
            b.Id,
            b.FlightId,
            b.PassengerId,
            b.TotalPrice,
            b.Status,
            b.CreatedAt,
            b.UpdatedAt);

    /// <summary>
    /// Creates a new booking.
    /// </summary>
    /// <param name="dto">The data transfer object for creating a booking.</param>
    /// <returns>The created booking represented as a BookingDto.</returns>
    public async Task<BookingDto> CreateBooking(CreateBookingDto dto)
    {
        var booking = Core.Entities.Booking.Create(dto.FlightId,
            dto.PassengerId,
            dto.TotalPrice,
            dto.Status);

        await _db.Bookings.AddAsync(booking);
        await _db.SaveChangesAsync();

        return EntityToDto.Compile().Invoke(booking);
    }

    /// <summary>
    /// Updates booking information if it exists; otherwise, returns null.
    /// </summary>
    /// <param name="id">The identifier of the booking to update.</param>
    /// <param name="dto">The data transfer object for updating a booking.</param>
    /// <returns>The updated booking represented as a BookingDto, or null if the booking is not found.</returns>
    public async Task<BookingDto?> UpdateBooking(long id, UpdateBookingDto dto)
    {
        var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        if (booking is null) return null;

        booking.Update(dto.TotalPrice, dto.Status);
        await _db.SaveChangesAsync();
        return EntityToDto.Compile().Invoke(booking);
    }

    /// <summary>
    /// Removes a booking from the database if it exists; otherwise, returns false.
    /// </summary>
    /// <param name="id">The identifier of the booking to delete.</param>
    /// <returns>True if the deletion is successful; otherwise, false.</returns>
    public async Task<bool> Delete(long id)
    {
        var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        if (booking is null)
        {
            _logger.LogWarning($"Booking with id {id} not found.");
            return false;
        }

        _db.Bookings.Remove(booking);
        return await _db.SaveChangesAsync() > 0;
    }
}