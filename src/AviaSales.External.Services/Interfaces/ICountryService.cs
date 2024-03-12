using AviaSales.External.Services.Models;

namespace AviaSales.External.Services.Interfaces;

public interface ICountryService
{
    Task<IEnumerable<RestCountry>> GetAll();
    Task<IEnumerable<RestCountry>> GetByName(string name);
    Task<IEnumerable<RestCountry>> GetByCode(string code);
    Task<IEnumerable<RestCountry>> GetByCapital(string capital);
}