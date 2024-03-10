using System.Linq.Expressions;
using AviaSales.Persistence;
using AviaSales.Shared.Managers;

namespace AviaSales.UseCases.Flight;

public class FlightManager : BaseManager<AviaSalesDb,Core.Entities.Flight,long,FlightDto>
{
    public FlightManager(AviaSalesDb db) : base(db)
    {
    }

    protected override Expression<Func<Core.Entities.Flight, FlightDto>> EntityToDto { get; }
}