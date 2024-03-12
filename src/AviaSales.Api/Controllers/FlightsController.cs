using AviaSales.Shared.Models;
using AviaSales.UseCases.Flight;
using Microsoft.AspNetCore.Mvc;

namespace AviaSales.Api.Controllers;

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
    /// Will return paged list of flights or  emptylist. 
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FlightDto>),200)]
    public async Task<IActionResult> GetList([FromQuery]ushort page,[FromQuery]byte perPage,
        [FromQuery]FlightFilters filters)
    {
        return Ok(await _manager.GetFlights(new Pager(page,perPage),filters));
    }
    
    /// <summary>
    /// Will return flight or  not found result. 
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FlightDto),200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute]long id)
    {
        var result = await _manager.GetById(id);
        return result is null ? NotFound() : Ok(result);
    }
}