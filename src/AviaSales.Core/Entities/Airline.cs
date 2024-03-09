using AviaSales.Shared.Entities;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents an airline entity with information about its code, name, and associated flights.
/// </summary>
public class Airline : Entity<long>
{
    // Private collection for storing flights associated with the airline.
    private readonly HashSet<Flight> _flights = new();

    // Private parameterless constructor to prevent direct instantiation.
    private Airline() { }

    /// <summary>
    /// Gets or sets the code of the airline.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// Gets or sets the name of the airline.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets a read-only collection of flights associated with the airline.
    /// </summary>
    public IReadOnlyCollection<Flight> Flights => _flights.ToList();

    /// <summary>
    /// Creates a new airline entity with the specified code and name.
    /// </summary>
    /// <param name="code">The code of the airline.</param>
    /// <param name="name">The name of the airline.</param>
    /// <returns>A new instance of the <see cref="Airline"/> class.</returns>
    public static Airline Create(string code, string name)
    {
        var airline = new Airline
        {
            Code = code,
            Name = name,
            CreatedAt = DateTime.UtcNow
        };

        return airline;
    }

    /// <summary>
    /// Adds a flight to the airline's collection of flights.
    /// </summary>
    /// <param name="flight">The flight to be added.</param>
    public void AddFlight(Flight flight)
    {
        _flights.Add(flight);
    }
}
