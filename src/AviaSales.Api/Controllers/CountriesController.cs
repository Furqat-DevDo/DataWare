using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Models;
using AviaSales.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace AviaSales.Api.Controllers;

/// <summary>
/// Controller for handling country-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _service;
    private readonly IDistributedCache _cache;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CountriesController"/> class.
    /// </summary>
    /// <param name="service">The country service.</param>
    /// <param name="cache">The distributed cache instance.</param>
    public CountriesController(ICountryService service, IDistributedCache cache)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }
    
    /// <summary>
    /// Retrieves a paginated list of countries.
    /// </summary>
    /// <param name="page">The page number for pagination.</param>
    /// <param name="perPage">The number of items per page.</param>
    /// <returns>An <see cref="IActionResult"/> containing the paginated list of countries.</returns>
    [HttpGet]
    [ResponseCache(Duration = 1200)]
    [ProducesResponseType(typeof(IEnumerable<RestCountry>), 200)]
    public async Task<IActionResult> GetCountries(ushort page, byte perPage)
    {
        var key = "countries";
        page = (ushort)(page <= 0 ? 1 : page);
        perPage = (byte)(perPage <= 0 ? 10 : perPage);
        
        _cache.TryGetValue<IEnumerable<RestCountry>>(key, out var response);

        if (response is null)
        {
            var result = await _service.GetAll();
            await _cache.SetAsync(key, result);
            
            var paged = result.Skip((page - 1) * perPage).Take(perPage);
            return Ok(paged);    
        }

        return Ok(response.Skip((page - 1) * perPage).Take(perPage));
    }
    
    /// <summary>
    /// Retrieves a list of countries by name.
    /// </summary>
    /// <param name="name">The name of the country.</param>
    /// <returns>An <see cref="IActionResult"/> containing the list of countries matching the provided name.</returns>
    [HttpGet("byname")]
    [ProducesResponseType(typeof(IEnumerable<RestCountry>), 200)]
    [ResponseCache(Duration = 1200)]
    public async Task<IActionResult> GetByName(string name)
    {
        var key = "byName";
        _cache.TryGetValue<IEnumerable<RestCountry>>(key, out var response);

        if (response is null)
        {
            var result = await _service.GetByName(name);
            await _cache.SetAsync(key, result);
            
            return Ok(result);    
        }
        
        return Ok(response);
    }
    
    /// <summary>
    /// Retrieves a list of countries by alpha code.
    /// </summary>
    /// <param name="code">The alpha code of the country.</param>
    /// <returns>An <see cref="IActionResult"/> containing the list of countries matching the provided alpha code.</returns>
    [HttpGet("bycode")]
    [ProducesResponseType(typeof(IEnumerable<RestCountry>), 200)]
    [ResponseCache(Duration = 1200)]
    public async Task<IActionResult> GetByCode(string code)
    {
        var key = "byCode";
        _cache.TryGetValue<IEnumerable<RestCountry>>(key, out var response);

        if (response is null)
        {
            var result = await _service.GetByCode(code);
            await _cache.SetAsync(key, result);
            
            return Ok(result);    
        }
        
        return Ok(response);
    }
    
    /// <summary>
    /// Retrieves a list of countries by capital.
    /// </summary>
    /// <param name="capital">The capital of the country.</param>
    /// <returns>An <see cref="IActionResult"/> containing the list of countries matching the provided capital.</returns>
    [HttpGet("bycapital")]
    [ProducesResponseType(typeof(IEnumerable<RestCountry>), 200)]
    [ResponseCache(Duration = 1200)]
    public async Task<IActionResult> GetByCapital(string capital)
    {
        var key = "byCapital";
        _cache.TryGetValue<IEnumerable<RestCountry>>(key, out var response);

        if (response is null)
        {
            var result = await _service.GetByCapital(capital);
            await _cache.SetAsync(key, result);
            
            return Ok(result);    
        }
        
        return Ok(response);
    }
}
