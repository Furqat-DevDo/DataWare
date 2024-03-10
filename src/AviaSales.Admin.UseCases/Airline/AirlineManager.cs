using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AviaSales.Admin.UseCases.Airline;

/// <summary>
/// Manager class for handling operations related to airlines in the AviaSales application.
/// </summary>
public class AirlineManager : BaseManager<AviaSalesDb, Core.Entities.Airline, long, AirlineDto>
{
    private readonly ILogger<AirlineManager> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AirlineManager"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <param name="logger">The logger for logging messages.</param>
    public AirlineManager(AviaSalesDb db, ILogger<AirlineManager> logger) : base(db)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets the expression to map Airline entities to AirlineDto objects.
    /// </summary>
    protected override Expression<Func<Core.Entities.Airline, AirlineDto>> EntityToDto =>
        a => new AirlineDto(
            a.Id,
            a.CreatedAt,
            a.IataCode,
            a.IcaoCode,
            a.Name);

    /// <summary>
    /// Creates a new airline.
    /// </summary>
    /// <param name="dto">The data transfer object for creating an airline.</param>
    /// <returns>The created airline represented as an AirlineDto.</returns>
    public async Task<AirlineDto> CreateAirline(CreateAirlineDto dto)
    {
        var airline = Core.Entities.Airline.Create(dto.IataCode, dto.IcaoCode, dto.Name);

        await _db.Airlines.AddAsync(airline);
        await _db.SaveChangesAsync();

        return EntityToDto.Compile().Invoke(airline);
    }

    /// <summary>
    /// Updates airline information if it exists; otherwise, returns null.
    /// </summary>
    /// <param name="id">The identifier of the airline to update.</param>
    /// <param name="dto">The data transfer object for updating an airline.</param>
    /// <returns>The updated airline represented as an AirlineDto, or null if the airline is not found.</returns>
    public async Task<AirlineDto?> UpdateAirline(long id, CreateAirlineDto dto)
    {
        var airline = await _db.Airlines.FirstOrDefaultAsync(a => a.Id == id);
        if (airline is null) return null;

        airline.Update(dto.IataCode, dto.IcaoCode, dto.Name);
        await _db.SaveChangesAsync();

        return EntityToDto.Compile().Invoke(airline);
    }

    /// <summary>
    /// Removes an airline from the database if it exists; otherwise, returns false.
    /// </summary>
    /// <param name="id">The identifier of the airline to delete.</param>
    /// <returns>True if the deletion is successful; otherwise, false.</returns>
    public async Task<bool> DeleteAirline(long id)
    {
        var airline = await _db.Airlines.FirstOrDefaultAsync(a => a.Id == id);
        if (airline is null)
        {
            _logger.LogWarning($"Airline with id {id} not found.");
            return false;
        }

        _db.Airlines.Remove(airline);
        return await _db.SaveChangesAsync() > 0;
    }
}
