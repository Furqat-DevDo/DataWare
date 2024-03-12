using AviaSales.UseCases.Country;
using Microsoft.AspNetCore.Mvc;

namespace AviaSales.Api.Controllers;

/// <summary>
/// Controller for handling country-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly CountryManager _manager;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CountriesController"/> class.
    /// </summary>
    ///<param name="manager">Countr manager.</param>
    public CountriesController(CountryManager manager)
    {
        _manager = manager;
    }
    
    /// <summary>
    /// Will return a list of countries or empty list.
    /// </summary>
    /// <param name="filters">Filters for searching country.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CountryDto>),200)]
    public async Task<IActionResult> GetList([FromQuery]CountryFilters filters)
    {
        return Ok(await _manager.GetList(filters));
    }
    
    /// <summary>
    /// Will return country with corresponding id or not found result.
    /// </summary>
    /// <param name="id">country's id.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CountryDto),200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(long id)
    {
        var country = await _manager.GetById(id);
        return country is null ? NotFound() : Ok(country);
    }
}
