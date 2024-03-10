using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AviaSales.Admin.UseCases.Booking;

public class BookingManager : BaseManager<AviaSalesDb,Core.Entities.Booking,long,BookingDto>
{
    private readonly ILogger<BookingManager> _logger;
    
    public BookingManager(AviaSalesDb db, ILogger<BookingManager> logger) : base(db)
    {
        _logger = logger;
    }

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
    /// Will create new booking. 
    /// </summary>
    /// <param name="dto">Create booking dto.</param>
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
    /// Will update booking info if ti exists else will return null.
    /// </summary>
    /// <param name="id">Booking's id.</param>
    /// <param name="dto"></param>
    public async Task<object?> UpdateBooking(long id, UpdateBookingDto dto)
    {
        var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        if (booking is null) return null;

        booking.Update(dto.TotalPrice,dto.Status);
        await _db.SaveChangesAsync();
        return EntityToDto.Compile().Invoke(booking);
    }

    public async Task<object?> Delete(long id)
    {
        throw new NotImplementedException();
    }
}