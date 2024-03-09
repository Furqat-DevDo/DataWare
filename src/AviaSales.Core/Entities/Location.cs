using Microsoft.EntityFrameworkCore;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents the geographical location of an airport.
/// </summary>
[Owned]
public class Location
{
    /// <summary>
    /// Gets or sets the latitude coordinate of the location.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the location.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Gets or sets the elevation above sea level (in meters).
    /// </summary>
    public int Elevation { get; set; }
}