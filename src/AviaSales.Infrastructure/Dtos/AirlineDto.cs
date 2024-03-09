namespace AviaSales.Infrastructure.Dtos;

/// <summary>
/// Data Transfer Object (DTO) for creating a new airline.
/// </summary>
/// <param name="iataCode">The IATA code of the airline.</param>
/// <param name="icaoCode">The ICAO code of the airline.</param>
/// <param name="Name">The name of the airline.</param>
public record CreateAirlineDto(string iataCode, string icaoCode, string Name);

/// <summary>
/// Data Transfer Object (DTO) for returning airline information.
/// </summary>
/// <param name="Id">The unique identifier in the database.</param>
/// <param name="CreatedAt">The entity's creation date with time.</param>
/// <param name="iataCode">The IATA code of the airline(International Air Transport Association).</param>
/// <param name="icaoCode">The ICAO code of the airline(International Civil Aviation Organization).</param>
/// <param name="Name">The name of the airline.</param>
public record AirlineDto(long Id, DateTime CreatedAt, string iataCode, string icaoCode, string Name);

