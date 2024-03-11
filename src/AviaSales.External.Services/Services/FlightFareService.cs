using System.Text;
using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Models;
using AviaSales.External.Services.Options;
using AviaSales.Shared.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AviaSales.External.Services.Services;

/// <summary>
/// Service for interacting with the Flight Fare API.
/// </summary>
public class FlightFareService : IFlightFareService
{
    private readonly IHttpClientFactory _factory;
    private readonly ILogger<FlightFareService> _logger;
    private readonly IOptions<FlightFareOptions> _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlightFareService"/> class.
    /// </summary>
    /// <param name="factory">Factory for creating HTTP clients.</param>
    /// <param name="logger">Logger for logging events.</param>
    /// <param name="options">Options for configuring the Flight Fare service.</param>
    public FlightFareService(
        IHttpClientFactory factory, 
        ILogger<FlightFareService> logger, 
        IOptions<FlightFareOptions> options)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Searches for flight fares based on the provided search parameters.
    /// </summary>
    /// <param name="search">The search parameters.</param>
    /// <returns>The flight fare result or an empty result if unsuccessful.</returns>
    /// <exception cref="Exception">Thrown when an external service exception occurs.</exception>
    public async Task<FlightFare> SearchFlight(SearchData search)
    {
        try
        {
            var client = _factory.CreateClient(nameof(FlightFareService));
            var url = CreateUrl(search);
            
            var result = await client.GetJsonAsync<FlightFare>(url);
            
            return result ?? new FlightFare();
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "External service exception has occurred.");
            throw;
        }
    }

    /// <summary>
    /// Creates the URL for the flight fare search based on the provided search parameters.
    /// </summary>
    /// <param name="search">The search parameters.</param>
    /// <returns>The constructed URL for the flight fare search.</returns>
    private string CreateUrl(SearchData search)
    {
        var urlBuilder = new StringBuilder($"/{_options.Value.ApiVersion}");
        
        // Add search parameters to the URL
        AddQueryParam(urlBuilder, "from", search.From);
        AddQueryParam(urlBuilder, "to", search.To);
        AddQueryParam(urlBuilder, "date", search.Date);
        AddQueryParam(urlBuilder, "type", search.Type);
        AddQueryParam(urlBuilder, "adult", search.Adult?.ToString());
        AddQueryParam(urlBuilder, "child", search.Child?.ToString());
        AddQueryParam(urlBuilder, "infant", search.Infant?.ToString());
        AddQueryParam(urlBuilder, "currency", search.Currency);

        return urlBuilder.ToString();
    }

    /// <summary>
    /// Adds a query parameter to the URL builder if the parameter value is not null or empty.
    /// </summary>
    /// <param name="urlBuilder">The StringBuilder used to construct the URL.</param>
    /// <param name="paramName">The name of the query parameter.</param>
    /// <param name="paramValue">The value of the query parameter.</param>
    private void AddQueryParam(StringBuilder urlBuilder, string paramName, string? paramValue)
    {
        if (!string.IsNullOrEmpty(paramValue))
        {
            urlBuilder.Append($"&{paramName}={Uri.EscapeDataString(paramValue)}");
        }
    }
}
