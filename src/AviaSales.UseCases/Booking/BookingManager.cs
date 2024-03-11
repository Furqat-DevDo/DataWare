using System.Linq.Expressions;
using AviaSales.External.Services.Interfaces;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using AviaSales.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AviaSales.UseCases.Booking;

/// <summary>
/// Manager class for handling operations related to bookings in the AviaSales application.
/// </summary>
public class BookingManager : BaseManager<AviaSalesDb, Core.Entities.Booking, long, BookingDto>
{
    private readonly ILogger<BookingManager> _logger;
    private readonly IFakeService _fakeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingManager"/> class.
    /// </summary>
    /// <param name="db">The database context.</param>
    /// <param name="logger">The logger for logging messages.</param>
    /// <param name="fakeService">Fake Service to represent external bookings.</param>
    public BookingManager(AviaSalesDb db, ILogger<BookingManager> logger, 
        IFakeService fakeService) : base(db)
    {
        _logger = logger;
        _fakeService = fakeService;
    }

    /// <summary>
    /// Gets the expression to map Booking entities to BookingDto objects.
    /// </summary>
    protected override Expression<Func<Core.Entities.Booking, BookingDto>> EntityToDto =>
        b => new BookingDto(
            b.Id,
            b.FlightId,
            b.PassengerId,
            b.TotalPrice,
            b.Status,
            b.CreatedAt,
            b.UpdatedAt);

    /// <summary>
    /// Creates a new booking based on the provided <paramref name="dto"/>.
    /// </summary>
    /// <param name="dto">The data transfer object containing booking details.</param>
    /// <returns>
    /// An <see cref="OperationResult{TData, TError}"/> with a <see cref="BookingDto"/> on success
    /// and a <see cref="ProblemDetails"/> on failure.
    /// </returns>
    public async Task<OperationResult<BookingDto,ProblemDetails>> CreateBooking(CreateBookingDto dto)
    {
        var flight = await _db.Flights
            .FirstOrDefaultAsync(f => f.Id == dto.FlightId);

        if (flight is null)
        {
            return OperationResult<BookingDto, ProblemDetails>.Failure(
                new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Instance = nameof(flight)
                });
        }

        var isAvailable = await _fakeService.CheckFlight(flight.Id);
        if (!isAvailable)
        {
            return OperationResult<BookingDto, ProblemDetails>.Failure(
                new ProblemDetails
                {
                    Status = StatusCodes.Status417ExpectationFailed,
                    Instance = nameof(flight)
                });
        }

        var bookingResult = await _fakeService.BookFlight(flight.Id);
        if (!bookingResult)
        {
            return OperationResult<BookingDto, ProblemDetails>.Failure(
                new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Instance = nameof(flight)
                });
        }

        var booking = Core.Entities.Booking.Create(dto.FlightId, dto.PassengerId, dto.TotalPrice, dto.Status);
    
        await _db.Bookings.AddAsync(booking);
        await _db.SaveChangesAsync();

        var bookingDto = EntityToDto.Compile().Invoke(booking);
        return OperationResult<BookingDto, ProblemDetails>.Success(bookingDto);
    }

    /// <summary>
    /// Updates an existing booking based on the provided ID and <paramref name="dto"/>.
    /// </summary>
    /// <param name="id">The ID of the booking to be updated.</param>
    /// <param name="dto">The data transfer object containing updated booking details.</param>
    /// <returns>
    /// An <see cref="OperationResult{TData, TError}"/> with a <see cref="BookingDto"/> on success
    /// and a <see cref="ProblemDetails"/> on failure.
    /// </returns>
    public async Task<OperationResult<BookingDto,ProblemDetails>> UpdateBooking(long id, UpdateBookingDto dto)
    {
        var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        if (booking is null)
        {
            _logger.LogWarning($"Booking with id {id} not found.");
            
            return OperationResult<BookingDto, ProblemDetails>.Failure(
                new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Instance = nameof(booking)
                });
        }

        var updateResult = await _fakeService.UpdateBooking(booking.Id);
        if (!updateResult)
        {
            _logger.LogWarning($"Update booking with id {id} failed.");
            
            return OperationResult<BookingDto, ProblemDetails>.Failure(
                new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Instance = nameof(booking)
                });
        }
        
        booking.Update(dto.TotalPrice, dto.Status);
        await _db.SaveChangesAsync();
        
        var bookingDto = EntityToDto.Compile().Invoke(booking);
        return OperationResult<BookingDto, ProblemDetails>.Success(bookingDto);
    }

    /// <summary>
    /// Deletes an existing booking based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the booking to be deleted.</param>
    /// <returns>
    /// An <see cref="OperationResult{TData, TError}"/> with a <see cref="bool"/> indicating success
    /// on deletion and a <see cref="ProblemDetails"/> on failure.
    /// </returns>
    public async Task<OperationResult<bool,ProblemDetails>> Delete(long id)
    {
        var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        
        if (booking is null)
        {
            _logger.LogWarning($"Booking with id {id} not found.");
            
            return OperationResult<bool, ProblemDetails>.Failure(
                new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Instance = nameof(booking)
                });
        }
        
        var deleteResult = await _fakeService.DeleteBooking(id);
        
        if (!deleteResult)
        {
            _logger.LogWarning($"Delete operation for booking with id : {id} failed.");
            return OperationResult<bool, ProblemDetails>.Failure(
            new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Instance = nameof(booking)
            });
        }
        
        _db.Bookings.Remove(booking);
        return OperationResult<bool, ProblemDetails>.Success(await _db.SaveChangesAsync() > 0);
    }
}