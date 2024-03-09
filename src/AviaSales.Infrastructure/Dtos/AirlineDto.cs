namespace AviaSales.Infrastructure.Dtos;

/// <summary>
/// Used for creation a new airline.
/// </summary>
/// <param name="Code">Airline code.</param>
/// <param name="Name">AirLine FullName</param>
public record CreateAirlineDto(string Code, string Name);

/// <summary>
/// Used For return information.
/// </summary>
/// <param name="Id">Unique Identifier in database.</param>
/// <param name="CreatedAt">Entity created date with time.</param>
/// <param name="Code">Airline's code.</param>
/// <param name="Name">Airline's name.</param>
public record AirlineDto(long Id, DateTime CreatedAt, string Code, string Name);
