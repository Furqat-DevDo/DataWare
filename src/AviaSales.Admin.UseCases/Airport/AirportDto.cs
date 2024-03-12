using AviaSales.Core.Enums;

namespace AviaSales.Admin.UseCases.Airport;

/// <summary>
/// Data transfer object for creating an airport.
/// </summary>
/// <param name="Code">The unique code assigned to the airport.</param>
/// <param name="TZ">The time zone of the airport.</param>
/// <param name="TimeZone">The time zone information of the airport.</param>
/// <param name="Type">The type of the airport (e.g., international, domestic).</param>
/// <param name="Label">The label or name associated with the airport.</param>
/// <param name="City">The city where the airport is located.</param>
/// <param name="CountryId">The country where the airport is located.</param>
/// <param name="Details">Additional details about the airport.</param>
/// <param name="Location">The geographical location (latitude and longitude) of the airport.</param>
public record CreateAirportDto(string Code, string TZ, string TimeZone, EAirportType Type, string Label, string City,
    long CountryId, AirportDetailsDto Details, LocationDto Location);

/// <summary>
/// Data transfer object for airport details.
/// </summary>
/// <param name="IataCode">The IATA code associated with the airport.</param>
/// <param name="IcaoCode">The ICAO code associated with the airport.</param>
/// <param name="Facilities">Additional facilities information about the airport.</param>
public record AirportDetailsDto(string IataCode, string IcaoCode, string Facilities);

/// <summary>
/// Data transfer object for representing the geographical location of an airport.
/// </summary>
/// <param name="Longtitude">The longitude coordinate of the airport.</param>
/// <param name="Latitude">The latitude coordinate of the airport.</param>
/// <param name="Elevation">The elevation of the airport above sea level.</param>
public record LocationDto(double Longtitude, double Latitude, int Elevation);

/// <summary>
/// Data transfer object for representing airport information.
/// </summary>
/// <param name="Id">The unique identifier of the airport.</param>
/// <param name="Code">The unique code assigned to the airport.</param>
/// <param name="TZ">The time zone of the airport.</param>
/// <param name="TimeZone">The time zone information of the airport.</param>
/// <param name="Type">The type of the airport (e.g., international, domestic).</param>
/// <param name="Label">The label or name associated with the airport.</param>
/// <param name="City">The city where the airport is located.</param>
/// <param name="CountryId">The country where the airport is located.</param>
/// <param name="Details">Additional details about the airport.</param>
/// <param name="Location">The geographical location (latitude and longitude) of the airport.</param>
public record AirportDto(long Id, string Code, string TZ, string TimeZone, EAirportType Type, string Label, string City,
    long CountryId, AirportDetailsDto Details, LocationDto Location);

