using System.Linq.Expressions;
using AviaSales.Core.Entities;
using AviaSales.Infrastructure.Dtos;
using AviaSales.Infrastructure.Persistance;
using AviaSales.Shared.Managers;
using Microsoft.Extensions.Logging;

namespace AviaSales.Infrastructure.Managers;

public class AirlineManager : BaseManager<AviaSalesDb,Airline,long,AirlineDto>
{
    private readonly ILogger<AirlineManager> _logger;
    
    public AirlineManager(AviaSalesDb db, ILogger<AirlineManager> logger) : base(db)
    {
        _logger = logger;
    }

    protected override Expression<Func<Airline, AirlineDto>> EntityToDto =>
        a => new AirlineDto(
            a.Id,
            a.CreatedAt,
            a.IataCode,
            a.IcaoCode,
            a.Name);

    public async Task<object> CreateAirline(CreateAirlineDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<object?> UpdateAirline(long id, CreateAirlineDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<object?> DeleteAirline(long id)
    {
        throw new NotImplementedException();
    }
}