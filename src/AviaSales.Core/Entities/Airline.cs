﻿using AviaSales.Shared.Entities;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents an airline entity with information about its IATA and ICAO codes, as well as its name.
/// </summary>
public class Airline : Entity<long>
{
    // Private parameterless constructor to prevent direct instantiation.
    private Airline() { }

    /// <summary>
    /// Gets or sets the IATA code of the airline.
    /// </summary>
    public string IataCode { get; private set; }
    
    /// <summary>
    /// Gets or sets the ICAO code of the airline.
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
}
