using Microsoft.EntityFrameworkCore;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents additional details of an airport.
/// </summary>
[Owned]
public class AirportDetails
{
    /// <summary>
    /// Gets or sets the IATA code for the airport.
    /// </summary>
    public required string IataCode { get; set; }

    /// <summary>
    /// Gets or sets the ICAO code for the airport.
    /// </summary>
    public required string IcaoCode { get; set; }

    /// <summary>
    /// Gets or sets a brief description or summary of the airport's facilities.
    /// </summary>
    public required string Facilities { get; set; }
}
