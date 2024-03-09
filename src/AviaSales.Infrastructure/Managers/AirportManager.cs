using System.Linq.Expressions;
using AviaSales.Core.Entities;
using AviaSales.Infrastructure.Dtos;
using AviaSales.Infrastructure.Persistance;
using AviaSales.Shared.Managers;
using Microsoft.EntityFrameworkCore;
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

    /// <summary>
    /// Will create a new airport.
    /// </summary>
    /// <param name="dto">Create airport dto.</param>
    public async Task<AirportDto> CreateAirport(CreateAirportDto dto)
    {
        var airport = Airport.Create(dto.Code,
            dto.TZ,
            dto.TimeZone,
            dto.Type,
            dto.Label,dto.City,
            dto.Country,
            new AirportDetails
            {
                IataCode = dto.Details.IataCode,
                IcaoCode = dto.Details.IcaoCode,
                Facilities = dto.Details.Facilities
            },
            new Location
            {
                Longitude = dto.Location.Longtitude,
                Latitude = dto.Location.Latitude,
                Elevation = dto.Location.Elevation
            });
        
        await _db.Airports.AddAsync(airport);
        await _db.SaveChangesAsync();
        
        return EntityToDto.Compile().Invoke(airport);
    }
    
    /// <summary>
    /// Will update airport information if it exist else will return null.
    /// </summary>
    /// <param name="id">Airport's id.</param>
    /// <param name="dto">Update Airport dto.</param>
    public async Task<AirportDto?> UpdateAirport(long id, CreateAirportDto dto)
    {
        var airport = await  _db.Airports.FirstOrDefaultAsync(a => a.Id == id);
        if (airport is null) return null;
        
        airport.Update(dto.Code,
            dto.TZ,
            dto.TimeZone,
            dto.Type,
            dto.Label,dto.City,
            dto.Country,
            new AirportDetails
            {
                IataCode = dto.Details.IataCode,
                IcaoCode = dto.Details.IcaoCode,
                Facilities = dto.Details.Facilities
            },
            new Location
            {
                Longitude = dto.Location.Longtitude,
                Latitude = dto.Location.Latitude,
                Elevation = dto.Location.Elevation
            });
        
        await _db.SaveChangesAsync();
        return EntityToDto.Compile().Invoke(airport);
    }
    
    /// <summary>
    /// Will remove airport from db if it exists else will reurn false.
    /// </summary>
    /// <param name="id">Airport's id.</param>
    public async Task<bool> Delete(long id)
    {
        var airport = await _db.Airports.FirstOrDefaultAsync(a => a.Id == id);
        if (airport is null)
        {
            _logger.LogWarning("Airport not found.");
            return false;
        }

        _db.Airports.Remove(airport);
        return await _db.SaveChangesAsync() > 0;
    }
}