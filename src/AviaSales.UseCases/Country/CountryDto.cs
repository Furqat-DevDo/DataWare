namespace AviaSales.UseCases.Country;

/// <summary>
/// Represents a Data Transfer Object (DTO) for presenting country information.
/// </summary>
/// <param name="id">The unique identifier of the country.</param>
/// <param name="Name">The name of the country.</param>
/// <param name="Cioc">The International Olympic Committee code of the country.</param>
/// <param name="Area">The total area of the country in square kilometers.</param>
/// <param name="Capital">The capital city of the country.</param>
/// <param name="Cca2">The two-letter country code based on the ISO 3166-1 alpha-2 standard.</param>
/// <param name="Cca3">The three-letter country code based on the ISO 3166-1 alpha-3 standard.</param>
/// <param name="Ccn3">The numeric country code based on the ISO 3166-1 numeric standard.</param>
public record CountryDto(long id, string Name, string Cioc, double? Area, string Capital, string Cca2, string Cca3, string Ccn3);

/// <summary>
/// Represents filters for querying countries based on name, capital, country code, and pagination.
/// </summary>
/// <param name="Name">The name of the country to filter by.</param>
/// <param name="Capital">The capital city of the country to filter by.</param>
/// <param name="Code">The country code to filter by.</param>
/// <param name="Pager">The pagination information for the query.</param>
public record CountryFilters(string? Name, string? Capital, string? Code,ushort?page,byte? perPage);
