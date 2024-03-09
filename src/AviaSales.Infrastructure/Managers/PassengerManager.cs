using System.Linq.Expressions;
using AviaSales.Core.Entities;
using AviaSales.Infrastructure.Dtos;
using AviaSales.Infrastructure.Persistance;
using AviaSales.Shared.Managers;
using Microsoft.Extensions.Logging;

namespace AviaSales.Infrastructure.Managers;

public class PassengerManager : BaseManager<AviaSalesDb,Passenger,long,PassengerDto>
{
    private readonly ILogger<PassengerManager> _logger;
    
    public PassengerManager(AviaSalesDb db, ILogger<PassengerManager> logger) : base(db)
    {
        _logger = logger;
    }

    protected override Expression<Func<Passenger, PassengerDto>> EntityToDto =>
        p => new PassengerDto(
            p.Id,
            p.UserId,
            p.FlightId,
            p.Email,
            p.Phone,
            p.Fullname,
            p.CreatedAt);
}