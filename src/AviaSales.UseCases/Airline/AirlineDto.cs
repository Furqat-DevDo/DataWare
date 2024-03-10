namespace AviaSales.UseCases.Airline;
/// <summary>
/// Data Transfer Object (DTO) for returning airline information.
/// </summary>
/// <param name="Id">The unique identifier in the database.</param>
/// <param name="CreatedAt">The entity's creation date with time.</param>
/// <param name="IataCode">The IATA code of the airline(International Air Transport Association).</param>
/// <param name="IcaoCode">The ICAO code of the airline(International Civil Aviation Organization).</param>
/// <param name="Name">The name of the airline.</param>
public record AirlineDto(long Id, DateTime CreatedAt, string IataCode, string IcaoCode, string Name);

/// <summary>
/// Represents a filter for airline information.
/// </summary>
/// <param name="Name">The name of the airline.</param>
/// <param name="IataCode">The International Air Transport Association (IATA) code of the airline. 
/// This is a three-letter code used to uniquely identify airlines in the aviation industry.</param>
/// <param name="IcaoCode">The International Civil Aviation Organization (ICAO) code of the airline. 
/// This is a four-letter alphanumeric code used to uniquely identify airlines in international aviation.</param>
public record AirlineFilter(string? Name, string? IataCode, string? IcaoCode);