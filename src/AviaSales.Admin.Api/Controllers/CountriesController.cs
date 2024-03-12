using AviaSales.Admin.UseCases.Country;
using AviaSales.Shared.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AviaSales.Admin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : Controller
{
    private readonly CountryManager _manager;
    private readonly IValidator<CreateCountryDto> _validator;

    public CountriesController(CountryManager manager, IValidator<CreateCountryDto> validator)
    {
        _manager = manager;
        _validator = validator;
    }

    /// <summary>
    /// Creates a new country based on the provided data.
    /// </summary>
    /// <param name="dto">Data for creating a new country.</param>
    /// <returns>Returns the newly created country.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CountryDto), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateCountryDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToProblemDetails());

        return Ok(await _manager.Craete(dto));
    }

    /// <summary>
    /// Updates an existing country with the provided data.
    /// </summary>
    /// <param name="id">Identifier of the country to update.</param>
    /// <param name="dto">Updated data for the country.</param>
    /// <returns>Returns the updated country.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CountryDto), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(long id, CreateCountryDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.ToProblemDetails());

        var update = await _manager.Update(id, dto);
        return update is null ? NotFound() : Ok(update);
    }

    /// <summary>
    /// Deletes a country based on the provided identifier.
    /// </summary>
    /// <param name="id">Identifier of the country to delete.</param>
    /// <returns>Returns 200 OK if the deletion is successful, otherwise returns 404 Not Found.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id)
    {
        var delete = await _manager.Delete(id);
        return delete ? Ok() : NotFound();
    }

    /// <summary>
    /// Retrieves a list of countries based on the provided filters.
    /// </summary>
    /// <param name="filters">Filters to apply when retrieving the list of countries.</param>
    /// <returns>Returns a collection of countries.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CountryDto>), 200)]
    public async Task<IActionResult> GetList([FromQuery]CountryFilters filters)
    {
        return Ok(await _manager.GetList(filters));
    }

    /// <summary>
    /// Retrieves a specific country based on the provided identifier.
    /// </summary>
    /// <param name="id">Identifier of the country to retrieve.</param>
    /// <returns>Returns the requested country if found, otherwise returns 404 Not Found.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CountryDto), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(long id)
    {
        var country = await _manager.GetById(id);
        return country is null ? NotFound() : Ok(country);
    }

}