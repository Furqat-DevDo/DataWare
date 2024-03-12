using AviaSales.Admin.UseCases.Airport;
using AviaSales.Shared.Extensions;
using AviaSales.Shared.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AviaSales.Admin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AirportsController : ControllerBase
{
    private readonly IValidator<CreateAirportDto> _validator;
    private readonly AirportManager _manager;
    
    public AirportsController(IValidator<CreateAirportDto> validator, AirportManager manager)
    {
        _validator = validator;
        _manager = manager;
    }
    
    /// <summary>
    /// Will create a new Airport.
    /// </summary>
    /// <param name="dto">Create Airport Dto.</param>
    [HttpPost]
    [ProducesResponseType(typeof(AirportDto),200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync(CreateAirportDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToProblemDetails());
        
        var result = await _manager.CreateAirport(dto);
        return Ok(result);
    }

    /// <summary>
    /// Will update existing airport's information.
    /// </summary>
    /// <param name="id">Airport's id.</param>
    /// <param name="dto">Update Airport Dto.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AirportDto),200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute]long id, CreateAirportDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToProblemDetails());
        
        var result = await _manager.UpdateAirport(id,dto);
        return result is null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Will delete airport from db . 
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
    /// Will return paged list of airports or  emptylist. 
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AirportDto>),200)]
    public async Task<IActionResult> GetList([FromQuery]ushort page,[FromQuery]byte perPage)
    {
        return Ok(await _manager.GetList(new Pager(page,perPage)));
    }
    
    /// <summary>
    /// Will return airport or  not found result. 
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AirportDto),200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute]long id)
    {
        var result = await _manager.GetById(id);
        return result is null ? NotFound() : Ok(result);
    }
}