using System.Linq.Expressions;
using AviaSales.Core.Entities;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AviaSales.Admin.UseCases.Airport;

/// <summary>
/// Manager class for handling operations related to airports in the AviaSales application.
/// </summary>
public class AirportManager : BaseManager<AviaSalesDb, Core.Entities.Airport, long, AirportDto>
{
    private readonly ILogger<AirportManager> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AirportManager"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <param name="logger">The logger for logging messages.</param>
    public AirportManager(AviaSalesDb db, ILogger<AirportManager> logger) : base(db)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets the expression to map Airport entities to AirportDto objects.
    /// </summary>
    protected override Expression<Func<Core.Entities.Airport, AirportDto>> EntityToDto =>
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
    /// Creates a new airport.
    /// </summary>
    /// <param name="dto">The data transfer object for creating an airport.</param>
    /// <returns>The created airport represented as an AirportDto.</returns>
    public async Task<AirportDto> CreateAirport(CreateAirportDto dto)
    {
        var airport = Core.Entities.Airport.Create(dto.Code,
            dto.TZ,
            dto.TimeZone,
            dto.Type,
            dto.Label,
            dto.City,
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
    /// Updates airport information if it exists; otherwise, returns null.
    /// </summary>
    /// <param name="id">The identifier of the airport to update.</param>
    /// <param name="dto">The data transfer object for updating an airport.</param>
    /// <returns>The updated airport represented as an AirportDto, or null if the airport is not found.</returns>
    public async Task<AirportDto?> UpdateAirport(long id, CreateAirportDto dto)
    {
        var airport = await _db.Airports.FirstOrDefaultAsync(a => a.Id == id);
        if (airport is null) return null;

        airport.Update(dto.Code,
            dto.TZ,
            dto.TimeZone,
            dto.Type,
            dto.Label,
            dto.City,
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
    /// Removes an airport from the database if it exists; otherwise, returns false.
    /// </summary>
    /// <param name="id">The identifier of the airport to delete.</param>
    /// <returns>True if the deletion is successful; otherwise, false.</returns>
    public async Task<bool> Delete(long id)
    {
        var airport = await _db.Airports.FirstOrDefaultAsync(a => a.Id == id);
        if (airport is null)
        {
            _logger.LogWarning($"Airport with id {id} not found.");
            return false;
        }

        _db.Airports.Remove(airport);
        return await _db.SaveChangesAsync() > 0;
    }
}
