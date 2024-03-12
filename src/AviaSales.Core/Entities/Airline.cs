using AviaSales.Shared.Entities;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents an airline entity with information about its IATA and ICAO codes, as well as its name.
/// </summary>
public class Airline : Entity<long>
{
    // Private parameterless constructor to prevent direct instantiation.
    private Airline() { }

    /// <summary>
    /// Gets or sets the International Air Transport Association (IATA) code of the airline.
    /// The IATA code is a two-character alphanumeric code assigned to airlines for easy identification.
    /// Example: "AA" for American Airlines.
    /// </summary>
    public string IataCode { get; private set; }

    /// <summary>
    /// Gets or sets the International Civil Aviation Organization (ICAO) code of the airline.
    /// The ICAO code is a four-character alphanumeric code assigned to airlines by the International Civil Aviation Organization.
    /// Example: "AAL" for American Airlines.
    /// </summary>
    public string IcaoCode { get; private set; }


    /// <summary>
    /// Gets or sets the name of the airline.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets a read-only collection of flights associated with the airline.
    /// </summary>
    public IReadOnlyCollection<Flight> Flights { get; set; }

    /// <summary>
    /// Creates a new airline entity with the specified IATA and ICAO codes, as well as the name.
    /// </summary>
    /// <param name="iataCode">The IATA code of the airline.</param>
    /// <param name="icaoCode">The ICAO code of the airline.</param>
    /// <param name="name">The name of the airline.</param>
    /// <returns>A new instance of the <see cref="Airline"/> class.</returns>
    public static Airline Create(string iataCode, string icaoCode, string name)
    {
        var airline = new Airline
        {
            IataCode = iataCode,
            IcaoCode = icaoCode,
            Name = name,
            CreatedAt = DateTime.UtcNow
        };

        return airline;
    }
    
    /// <summary>
    /// Updates the properties of the entity.
    /// </summary>
    /// <param name="iataCode">The new IATA code.</param>
    /// <param name="icaoCode">The new ICAO code.</param>
    /// <param name="name">The new name.</param>
    public void Update(string iataCode, string icaoCode, string name)
    {
        IataCode = iataCode;
        IcaoCode = icaoCode;
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}
