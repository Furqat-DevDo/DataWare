using AviaSales.Admin.Api.Extensions;
using AviaSales.Admin.UseCases.Flight;
using AviaSales.Shared.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AviaSales.Admin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly FlightManager _manager;
    
    public FlightsController(FlightManager manager)
    {
        _manager = manager;
    }

    /// <summary>
    /// Will create a new Flight.
    /// </summary>
    /// <param name="dto">Create Flight Dto.</param>
    /// <param name="validator">Craete flight validator.</param>
    [HttpPost]
    [ProducesResponseType(typeof(FlightDto),200)]
    [ProducesResponseType(typeof(ProblemDetails),400)]
    public async Task<IActionResult> CreateAsync(CreateFlightDto dto,
        [FromServices]IValidator<CreateFlightDto> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToProblemDetails());
        
        var result = await _manager.CreateFlight(dto);
        return Ok(result);
    }

    /// <summary>
    /// Will update existing flight's information.
    /// </summary>
    /// <param name="id">Flight's id.</param>
    /// <param name="dto">Update Flight Dto.</param>
    /// <param name="validator">Update Flight validator.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(FlightDto),200)]
    [ProducesResponseType(typeof(ProblemDetails),400)]
    public async Task<IActionResult> UpdateAsync([FromRoute]long id, UpdateFlightDto dto,
        [FromServices]IValidator<UpdateFlightDto> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToProblemDetails());
        
        var result = await _manager.UpdateFlight(id,dto);
        return result is null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Will delete flight from db . 
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute]long id)
    {
        var result = await _manager.Delete(id);
        return result ? Ok() : NotFound();
    }
    
    /// <summary>
    /// Will return paged list of flights or  emptylist. 
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FlightDto>),200)]
    public async Task<IActionResult> GetList([FromQuery]ushort page,[FromQuery]byte perPage)
    {
        return Ok(await _manager.GetList(new Pager(page,perPage)));
    }
    
    /// <summary>
    /// Will return flight or  not found result. 
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FlightDto),200)]
    [ProducesResponseType(typeof(NotFoundObjectResult),404)]
    public async Task<IActionResult> Get([FromRoute]long id)
    {
        var result = await _manager.GetById(id);
        return result is null ? NotFound() : Ok(result);
    }
}