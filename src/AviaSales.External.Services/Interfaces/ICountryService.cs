using AviaSales.External.Services.Models;

namespace AviaSales.External.Services.Interfaces;

public interface ICountryService
{
    Task<IEnumerable<Country>> GetAll();
    Task<IEnumerable<Country>> GetByName(string name);
    Task<IEnumerable<Country>> GetByCode(string code);
    Task<IEnumerable<Country>> GetByCapital(string capital);
}