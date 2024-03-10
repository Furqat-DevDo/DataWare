using AviaSales.Api.Extensions;
using AviaSales.UseCases.Passenger;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AviaSales.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PassengersController : ControllerBase
{
    private readonly IValidator<CreatePassengerDto> _validator;
    private readonly PassengerManager _manager;
    
    public PassengersController(IValidator<CreatePassengerDto> validator, PassengerManager manager)
    {
        _validator = validator;
        _manager = manager;
    }
    
    /// <summary>
    /// Will create a new Passenger.
    /// </summary>
    /// <param name="dto">Create Passenger Dto.</param>
    [HttpPost]
    [ProducesResponseType(typeof(PassengerDto),200)]
    [ProducesResponseType(typeof(ProblemDetails),400)]
    public async Task<IActionResult> CreateAsync(CreatePassengerDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToProblemDetails());
        
        var result = await _manager.CreatePassenger(dto);
        return Ok(result);
    }

    /// <summary>
    /// Will update existing passenger's information.
    /// </summary>
    /// <param name="id">Passenger's id.</param>
    /// <param name="dto">Update Passenger Dto.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PassengerDto),200)]
    [ProducesResponseType(typeof(ProblemDetails),400)]
    public async Task<IActionResult> UpdateAsync([FromRoute]long id, CreatePassengerDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToProblemDetails());
        
        var result = await _manager.UpdatePassenger(id,dto);
        return result is null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Will return passenger or  not found result. 
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PassengerDto),200)]
    [ProducesResponseType(typeof(NotFoundObjectResult),404)]
    public async Task<IActionResult> Get([FromRoute]long id)
    {
        var result = await _manager.GetById(id);
        return result is null ? NotFound() : Ok(result);
    }
}