using AviaSales.Admin.UseCases.Country;
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

    [HttpPost]
    public async Task<IActionResult> Create(CreateCountryDto dto)
    {
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id,CreateCountryDto dto)
    {
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        return Ok();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        return Ok();
    }
}