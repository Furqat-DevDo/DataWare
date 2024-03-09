using AviaSales.API.Extensions;
using AviaSales.Infrastructure.Dtos;
using AviaSales.Infrastructure.Managers;
using AviaSales.Shared.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AviaSales.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AirlinesController : ControllerBase
{
    private readonly IValidator<CreateAirlineDto> _validator;
    private readonly AirlineManager _manager;
    
    public AirlinesController(IValidator<CreateAirlineDto> validator, AirlineManager manager)
    {
        _validator = validator;
        _manager = manager;
    }

    /// <summary>
    /// Will create a new Airline.
    /// </summary>
    /// <param name="dto">Create Airline Dto.</param>
    [HttpPost]
    [ProducesResponseType(typeof(AirlineDto),200)]
    [ProducesResponseType(typeof(ProblemDetails),400)]
    public async Task<IActionResult> CreateAsync(CreateAirlineDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToProblemDetails());
        
        var result = await _manager.CreateAirline(dto);
        return Ok(result);
    }

    /// <summary>
    /// Will update existing airline's information.
    /// </summary>
    /// <param name="id">Airline's id.</param>
    /// <param name="dto">Update Airline Dto.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AirlineDto),200)]
    [ProducesResponseType(typeof(ProblemDetails),400)]
    public async Task<IActionResult> UpdateAsync([FromRoute]long id, CreateAirlineDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToProblemDetails());
        
        var result = await _manager.UpdateAirline(id,dto);
        return result is null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Will delete existing airline from db.
    /// </summary>
    /// <param name="id">Airline's id.</param>
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute]long id)
    {
        var result = await _manager.DeleteAirline(id);
        return result ? NotFound() : Ok();
    }

    /// <summary>
    /// Will return list of airlines or empty list.
    /// </summary>
    /// <param name="page">Page number.</param>
    /// <param name="perPage">Elements count per page.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AirlineDto>),200)]
    public async Task<IActionResult> GetListAsync([FromQuery]ushort page,[FromQuery]byte perPage)
    {
        return Ok(await _manager.GetList(new Pager(page,perPage)));
    }

    /// <summary>
    /// Will return airline or not found result.
    /// </summary>
    /// <param name="id"> Airline's id.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AirlineDto),200)]
    [ProducesResponseType(typeof(NotFoundObjectResult),404)]
    public async Task<IActionResult> GetAsync([FromRoute] long id)
    {
        var result = await _manager.GetById(id);
        return result is null ? NotFound() : Ok(result);
    }
}