using AviaSales.Shared.Models;
using AviaSales.UseCases.Airport;
using Microsoft.AspNetCore.Mvc;

namespace AviaSales.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AirportsController : ControllerBase
{
    private readonly AirportManager _manager;
    
    public AirportsController(AirportManager manager)
    {
        _manager = manager;
    }
    
    /// <summary>
    /// Will return paged list of airports or  emptylist. 
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AirportDto>),200)]
    public async Task<IActionResult> GetList(ushort page,byte perPage,[FromQuery]AirportFilter filter)
    {
        return Ok(await _manager.GetAirports(new Pager(page,perPage),filter));
    }
    
    /// <summary>
    /// Will return airport or  not found result. 
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AirportDto),200)]
    [ProducesResponseType(typeof(NotFoundObjectResult),404)]
    public async Task<IActionResult> Get([FromRoute]long id)
    {
        var result = await _manager.GetById(id);
        return result is null ? NotFound() : Ok(result);
    }
}