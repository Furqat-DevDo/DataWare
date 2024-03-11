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
    public string IataCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ICAO code for the airport.
    /// </summary>
    public string IcaoCode { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a brief description or summary of the airport's facilities.
    /// </summary>
    public string Facilities { get; set; } = string.Empty;
}
