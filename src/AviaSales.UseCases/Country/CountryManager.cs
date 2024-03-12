using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;

namespace AviaSales.UseCases.Country;

/// <summary>
/// Manages operations related to the 'Country' entity, serving as an intermediary between the data access layer and higher-level services.
/// </summary>
public class CountryManager : BaseManager<AviaSalesDb, Core.Entities.Country, long, CountryDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CountryManager"/> class.
    /// </summary>
    /// <param name="db">The database context used for data access.</param>
    public CountryManager(AviaSalesDb db) : base(db)
    {
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
}
