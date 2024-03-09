using System.Linq.Expressions;
using AviaSales.Core.Entities;
using AviaSales.Infrastructure.Dtos;
using AviaSales.Infrastructure.Persistance;
using AviaSales.Shared.Managers;
using Microsoft.Extensions.Logging;

namespace AviaSales.Infrastructure.Managers;

public class BookingManager : BaseManager<AviaSalesDb,Booking,long,BookingDto>
{
    private readonly ILogger<BookingManager> _logger;
    
    public BookingManager(AviaSalesDb db, ILogger<BookingManager> logger) : base(db)
    {
        _logger = logger;
    }

    protected override Expression<Func<Booking, BookingDto>> EntityToDto =>
        b => new BookingDto(
            b.Id,
            b.FlightId,
            b.PassengerId,
            b.TotalPrice,
            b.Status,
            b.CreatedAt,
            b.UpdatedAt);
}