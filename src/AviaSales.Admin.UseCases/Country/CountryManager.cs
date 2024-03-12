using System.Linq.Expressions;
using AviaSales.External.Services.Interfaces;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;
using AviaSales.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AviaSales.Admin.UseCases.Country;

public class CountryManager : BaseManager<AviaSalesDb,Core.Entities.Country,long,CountryDto>
{
    private readonly ILogger<CountryManager> _logger;
    private readonly ICountryService _resCountrService;
    
    public CountryManager(AviaSalesDb db, 
        ILogger<CountryManager> logger, 
        ICountryService resCountrService) : base(db)
    {
        _logger = logger;
        _resCountrService = resCountrService;
    }

    protected override Expression<Func<Core.Entities.Country, CountryDto>> EntityToDto =>
        c => new CountryDto(
            c.Id, 
            c.Name, 
            c.Cioc, 
            c.Area, 
            c.Capital, 
            c.Cca2, 
            c.Cca3, 
            c.Ccn3);

    public async Task<OperationResult<CountryDto, ProblemDetails>> Craete(CreateCountryDto dto)
    {
        throw new NotImplementedException();
    }
    
    public async Task<OperationResult<CountryDto, ProblemDetails>> Update(CreateCountryDto dto)
    {
        throw new NotImplementedException();
    }
    
    public async Task<OperationResult<CountryDto, ProblemDetails>> Delete(CreateCountryDto dto)
    {
        throw new NotImplementedException();
    }
    
}