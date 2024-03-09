using System.Linq.Expressions;
using AviaSales.Core.Entities;
using AviaSales.Infrastructure.Dtos;
using AviaSales.Infrastructure.Persistance;
using AviaSales.Shared.Managers;
using Microsoft.EntityFrameworkCore;
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
    
    /// <summary>
    /// Will create a new airline.
    /// </summary>
    /// <param name="dto">create airline dto.</param>
    public async Task<AirlineDto> CreateAirline(CreateAirlineDto dto)
    {
        var airline = Airline.Create(dto.IataCode,dto.IcaoCode,dto.Name);
        
        await _db.Airlines.AddAsync(airline);
        
        return EntityToDto.Compile().Invoke(airline);
    }

    /// <summary>
    /// Will update airline informations if it exist else it will return null.
    /// </summary>
    /// <param name="id">Airline's id.</param>
    /// <param name="dto">Update Airline Dto.</param>
    public async Task<AirlineDto?> UpdateAirline(long id, CreateAirlineDto dto)
    {
        var airline = await _db.Airlines.FirstOrDefaultAsync( a => a.Id == id);
        if (airline is null) return null;

        airline.Update(dto.IataCode,dto.IcaoCode,dto.Name);
        await _db.SaveChangesAsync();

        return EntityToDto.Compile().Invoke(airline);
    }
    
    /// <summary>
    /// Will remove airline if it exist else it will return false.
    /// </summary>
    /// <param name="id">Airline's id.</param>
    public async Task<bool> DeleteAirline(long id)
    {
        var airline = await _db.Airlines.FirstOrDefaultAsync(a => a.Id == id);
        if (airline is null)
        {
            _logger.LogWarning("Airline not found.");
            return false;
        }

        _db.Airlines.Remove(airline);
        return await _db.SaveChangesAsync() > 0;
    }
}