using AviaSales.Shared.Models;
using AviaSales.UseCases.Airline;
using Microsoft.AspNetCore.Mvc;

namespace AviaSales.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AirlinesController : ControllerBase
{
    private readonly AirlineManager _manager;
    
    public AirlinesController(AirlineManager manager)
    {
        _manager = manager;
    }

    /// <summary>
    /// Will return list of airlines or empty list.
    /// </summary>
    /// <param name="page">Page number.</param>
    /// <param name="perPage">Elements count per page.</param>
    /// <param name="filter">Filters</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AirlineDto>),200)]
    public async Task<IActionResult> GetListAsync(ushort page,byte perPage,[FromQuery]AirlineFilter filter)
    {
        return Ok(await _manager.GetAirLines(new Pager(page,perPage),filter));
    }

    /// <summary>
    /// Will return airline or not found result.
    /// </summary>
    /// <param name="id"> Airline's id.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AirlineDto),200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute] long id)
    {
        var result = await _manager.GetById(id);
        return result is null ? NotFound() : Ok(result);
    }
}