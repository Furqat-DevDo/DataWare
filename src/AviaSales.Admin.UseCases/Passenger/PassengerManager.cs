using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using Microsoft.Extensions.Logging;

namespace AviaSales.Admin.UseCases.Passenger;

public class PassengerManager : BaseManager<AviaSalesDb,Core.Entities.Passenger,long,PassengerDto>
{
    private readonly ILogger<PassengerManager> _logger;
    
    public PassengerManager(AviaSalesDb db, ILogger<PassengerManager> logger) : base(db)
    {
        _logger = logger;
    }

    protected override Expression<Func<Core.Entities.Passenger, PassengerDto>> EntityToDto =>
        p => new PassengerDto(
            p.Id,
            p.UserId,
            p.FlightId,
            p.Email,
            p.Phone,
            p.Fullname,
            p.CreatedAt);
    public async Task<object?> CreatePassenger(CreatePassengerDto dto)
    {
        throw new NotImplementedException();
    }
    
    public async Task<object?> UpdatePassenger(long id, CreatePassengerDto dto)
    {
        throw new NotImplementedException();
    }
    
    public async Task<object?> Delete(long id)
    {
        throw new NotImplementedException();
    }
}
