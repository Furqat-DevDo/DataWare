using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AviaSales.Admin.UseCases.Passenger;

/// <summary>
/// Manager class for handling operations related to passengers in the AviaSales application.
/// </summary>
public class PassengerManager : BaseManager<AviaSalesDb, Core.Entities.Passenger, long, PassengerDto>
{
    private readonly ILogger<PassengerManager> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PassengerManager"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <param name="logger">The logger for logging messages.</param>
    public PassengerManager(AviaSalesDb db, ILogger<PassengerManager> logger) : base(db)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets the expression to map Passenger entities to PassengerDto objects.
    /// </summary>
    protected override Expression<Func<Core.Entities.Passenger, PassengerDto>> EntityToDto =>
        p => new PassengerDto(
            p.Id,
            p.UserId,
            p.FlightId,
            p.Email,
            p.Phone,
            p.Fullname,
            p.CreatedAt);

    /// <summary>
    /// Creates a new passenger.
    /// </summary>
    /// <param name="dto">The data transfer object for creating a passenger.</param>
    /// <returns>The created passenger represented as a PassengerDto.</returns>
    public async Task<PassengerDto> CreatePassenger(CreatePassengerDto dto)
    {
        var passenger = Core.Entities.Passenger.Create(dto.UserId,
            dto.FlightId,
            dto.Email,
            dto.Phone,
            dto.Fullname);

        await _db.Passengers.AddAsync(passenger);
        await _db.SaveChangesAsync();

        return EntityToDto.Compile().Invoke(passenger);
    }

    /// <summary>
    /// Updates an existing passenger.
    /// </summary>
    /// <param name="id">The identifier of the passenger to update.</param>
    /// <param name="dto">The data transfer object for updating a passenger.</param>
    /// <returns>The updated passenger represented as a PassengerDto, or null if the passenger is not found.</returns>
    public async Task<PassengerDto?> UpdatePassenger(long id, CreatePassengerDto dto)
    {
        var passenger = await _db.Passengers.FirstOrDefaultAsync(p => p.Id == id);
        if (passenger is null) return null;

        passenger.Update(dto.UserId, dto.FlightId, dto.Phone, dto.Email, dto.Fullname);
        await _db.SaveChangesAsync();

        return EntityToDto.Compile().Invoke(passenger);
    }

    /// <summary>
    /// Deletes a passenger with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the passenger to delete.</param>
    /// <returns>True if the deletion is successful; otherwise, false.</returns>
    public async Task<bool> Delete(long id)
    {
        var passenger = await _db.Passengers.FirstOrDefaultAsync(p => p.Id == id);
        if (passenger is null)
        {
            _logger.LogWarning($"Passenger with id {id} not found.");
            return false;
        }

        _db.Passengers.Remove(passenger);
        return await _db.SaveChangesAsync() > 0;
    }
}

