using AviaSales.Core.Enums;
using AviaSales.Shared.Entities;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents an airport entity with comprehensive information about its code, timezone, type, label, location, and facilities.
/// </summary>
public class Airport : Entity<long>
{
    private Airport(){}
    
    /// <summary>
    /// Gets or sets the unique code associated with the airport.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// Gets or sets the timezone abbreviation for the airport.
    /// </summary>
    public string TZ { get; private set; }

    /// <summary>
    /// Gets or sets the full timezone name for the airport.
    /// </summary>
    public string TimeZone { get; private set; }

    /// <summary>
    /// Gets or sets the type of the airport.
    /// </summary>
    public EAirportType Type { get; private set; }

    /// <summary>
    /// Gets or sets the label or name associated with the airport.
    /// </summary>
    public string Label { get; private set; }

    /// <summary>
    /// Gets or sets the city where the airport is located.
    /// </summary>
    public string City { get; private set; }

    /// <summary>
    /// Gets or sets the country where the airport is situated.
    /// </summary>
    public string Country { get; private set; }
    
    /// <summary>
    /// Gets or sets the location details of the airport.
    /// </summary>
    public Location Location { get; set; }

    /// <summary>
    /// Gets or sets additional details of the airport.
    /// </summary>
    public AirportDetails Details { get; set; }

    public static Airport Create(string code, string tz, string timeZone, EAirportType type, string label, string city, 
        string country,AirportDetails details,Location location)
    {
        var airport = new Airport
        {
            Code = code,
            TZ = tz,
            TimeZone = timeZone,
            Type = type,
            Label = label,
            City = city,
            Country = country,
            Details = details,
            Location = location
        };

        return airport;
    }
}
