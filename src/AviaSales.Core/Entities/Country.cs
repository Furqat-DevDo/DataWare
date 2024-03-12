using AviaSales.Shared.Entities;

namespace AviaSales.Core.Entities;

/// <summary>
/// Represents a country entity with information such as name, code, area, capital, and associated airports.
/// </summary>
public class Country : Entity<long>
{
    /// <summary>
    /// Private parameterless constructor used for entity creation by Entity Framework.
    /// </summary>
    private Country(){}

    /// <summary>
    /// Gets or sets the name of the country.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets or sets the International Olympic Committee code of the country.
    /// </summary>
    public string Cioc { get; private set; }
    
    /// <summary>
    /// Gets or sets the two-letter country code based on the ISO 3166-1 alpha-2 standard.
    /// </summary>
    /// <remarks>
    /// The alpha-2 code uniquely represents a country using two letters (e.g., "US" for the United States, "CA" for Canada).
    /// </remarks>
    /// <value>The two-letter country code.</value>
    public string Cca2 { get; set; }

    /// <summary>
    /// Gets or sets the numeric country code based on the ISO 3166-1 numeric standard.
    /// </summary>
    /// <remarks>
    /// The numeric code uniquely represents a country using three digits (e.g., "840" for the United States).
    /// </remarks>
    /// <value>The numeric country code.</value>
    public string Ccn3 { get; set; }

    /// <summary>
    /// Gets or sets the three-letter country code based on the ISO 3166-1 alpha-3 standard.
    /// </summary>
    /// <remarks>
    /// The alpha-3 code uniquely represents a country using three letters (e.g., "USA" for the United States).
    /// </remarks>
    /// <value>The three-letter country code.</value>
    public string Cca3 { get; set; }


    /// <summary>
    /// Gets or sets the total area of the country in square kilometers.
    /// </summary>
    public double? Area { get; private set; }

    /// <summary>
    /// Gets or sets the capital city of the country.
    /// </summary>
    public string Capital { get; private set; }

    /// <summary>
    /// Gets the list of airports associated with the country.
    /// </summary>
    public IReadOnlyList<Airport> Airports { get; private set; }

    /// <summary>
    /// Creates a new instance of the <see cref="Country"/> class.
    /// </summary>
    /// <param name="name">The name of the country.</param>
    /// <param name="cioc">The International Olympic Committee code of the country.</param>
    /// <param name="area">The total area of the country in square kilometers.</param>
    /// <param name="capital">The capital city of the country.</param>
    /// <param name="cca2">The alpha-2 code uniquely represents a country using two letters (e.g., "US" for the United States, "CA" for Canada).</param>
    /// <param name="cca3">The alpha-3 code uniquely represents a country using three letters (e.g., "USA" for the United States).</param>
    /// <param name="ccn3">The numeric code uniquely represents a country using three digits (e.g., "840" for the United States).</param>
    /// <returns>The newly created <see cref="Country"/> instance.</returns>
    public static Country Create(string name, string cioc, double? area, string capital, string cca2, string cca3, string ccn3)
    {
        var country = new Country
        {
            Name = name,
            Cioc = cioc,
            Area = area,
            Capital = capital,
            Cca2 = cca2,
            Ccn3 = ccn3,
            Cca3 = cca3,
            CreatedAt = DateTime.UtcNow
        };

        return country;
    }

    /// <summary>
    /// Updates the properties of the country.
    /// </summary>
    /// <param name="name">The new name of the country.</param>
    /// <param name="cioc">The new International Olympic Committee code of the country.</param>
    /// <param name="area">The new total area of the country in square kilometers.</param>
    /// <param name="capital">The new capital city of the country.</param>
    /// <param name="cca2">The alpha-2 code uniquely represents a country using two letters (e.g., "US" for the United States, "CA" for Canada).</param>
    /// <param name="cca3">The alpha-3 code uniquely represents a country using three letters (e.g., "USA" for the United States).</param>
    /// <param name="ccn3">The numeric code uniquely represents a country using three digits (e.g., "840" for the United States).</param>
    public void Update(string name, string cioc, double? area, string capital,string cca2, string cca3, string ccn3)
    {
        Name = name;
        Cioc = cioc;
        Area = area;
        Capital = capital;
        Cca2 = cca2;
        Cca3 = cca3;
        Ccn3 = ccn3;
        UpdatedAt = DateTime.UtcNow;
    }
}
