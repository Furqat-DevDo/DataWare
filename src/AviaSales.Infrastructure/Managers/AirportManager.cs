using System.Linq.Expressions;
using AviaSales.Core.Entities;
using AviaSales.Infrastructure.Dtos;
using AviaSales.Infrastructure.Persistance;
using AviaSales.Shared.Managers;
using Microsoft.Extensions.Logging;

namespace AviaSales.Infrastructure.Managers;

public class AirportManager : BaseManager<AviaSalesDb,Airport,long,AirportDto>
{
    private readonly ILogger<AirportManager> _logger;
    
    public AirportManager(AviaSalesDb db, ILogger<AirportManager> logger) : base(db)
    {
        _logger = logger;
    }

    protected override Expression<Func<Airport, AirportDto>> EntityToDto =>
        ai => new AirportDto(
            ai.Id,
            ai.Code,
            ai.TZ,
            ai.TimeZone,
            ai.Type,
            ai.Label,
            ai.City,
            ai.Country,
            new AirportDetailsDto(
                ai.Details.IataCode,
                ai.Details.IcaoCode,
                ai.Details.Facilities),
            new LocationDto(
                ai.Location.Longitude,
                ai.Location.Latitude,
                ai.Location.Elevation));
}