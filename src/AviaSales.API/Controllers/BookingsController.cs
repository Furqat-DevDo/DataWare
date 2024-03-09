using AviaSales.API.Extensions;
using AviaSales.Infrastructure.Dtos;
using AviaSales.Infrastructure.Managers;
using AviaSales.Shared.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AviaSales.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly BookingManager _manager;
    
    public BookingsController(BookingManager manager)
    {
        _manager = manager;
    }

    /// <summary>
    /// Will create a new Booking.
    /// </summary>
    /// <param name="dto">Create Booking Dto.</param>
    /// <param name="validator">Create Booking validator</param>
    [HttpPost]
    [ProducesResponseType(typeof(BookingDto),200)]
    [ProducesResponseType(typeof(ProblemDetails),400)]
    public async Task<IActionResult> CreateAsync(CreateBookingDto dto,
        [FromServices] IValidator<CreateBookingDto> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToProblemDetails());
        
        var result = await _manager.CreateBooking(dto);
        return Ok(result);
    }

    /// <summary>
    /// Will update existing booking's information.
    /// </summary>
    /// <param name="id">Booking's id.</param>
    /// <param name="dto">Update Booking Dto.</param>
    /// <param name="validator">Update Booking validator.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BookingDto),200)]
    [ProducesResponseType(typeof(ProblemDetails),400)]
    public async Task<IActionResult> UpdateAsync([FromRoute]long id, UpdateBookingDto dto,
        [FromServices]IValidator<UpdateBookingDto> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToProblemDetails());
        
        var result = await _manager.UpdateBooking(id,dto);
        return result is null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Will delete booking from db . 
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute]long id)
    {
        var result = await _manager.Delete(id);
        return result is null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Will return paged list of bookings or  emptylist. 
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookingDto>),200)]
    public async Task<IActionResult> GetList([FromQuery]ushort page,[FromQuery]byte perPage)
    {
        return Ok(await _manager.GetList(new Pager(page,perPage)));
    }
    
    /// <summary>
    /// Will return booking or  not found result. 
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookingDto),200)]
    [ProducesResponseType(typeof(NotFoundObjectResult),404)]
    public async Task<IActionResult> Get([FromRoute]long id)
    {
        var result = await _manager.GetById(id);
        return result is null ? NotFound() : Ok(result);
    }
}