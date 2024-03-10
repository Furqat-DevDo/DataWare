﻿using System.Text.Json;
using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Models;
using AviaSales.External.Services.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AviaSales.External.Services.Services;

/// <summary>
/// Represents a client for interacting with the Rest Countries API.
/// </summary>
public class RestCountryService : ICountryService
{
    private readonly IHttpClientFactory _factory;
    private readonly ILogger<RestCountryService> _logger;
    private readonly IOptions<RestCountryOptions> _options;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="RestCountryService"/> class.
    /// </summary>
    /// <param name="logger">The logger for logging events.</param>
    /// <param name="options">The options for configuring the Rest Country client.</param>
    /// <param name="factory"></param>
    public RestCountryService(ILogger<RestCountryService> logger, IOptions<RestCountryOptions> options,IHttpClientFactory factory)
    {
        _logger = logger;
        _options = options;
        _factory = factory;
    }

    /// <summary>
    /// Deserializes JSON content from a stream asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of object to deserialize.</typeparam>
    /// <param name="responseStream">The stream containing the JSON content.</param>
    /// <returns>The deserialized object or default value if deserialization fails.</returns>
    private async Task<T?> DeserializeJsonAsync<T>(Stream responseStream)
    {
        return await JsonSerializer.DeserializeAsync<T>(responseStream) ?? default!;
    }

    /// <summary>
    /// Performs an HTTP request for a list of countries based on the provided URL.
    /// </summary>
    /// <param name="url">The URL for the countries request.</param>
    /// <returns>The deserialized list of <see cref="Country"/> objects.</returns>
    private async Task<IEnumerable<Country>> PerformCountryRequestAsync(string url)
    {
        try
        {
            var client = _factory.CreateClient(nameof(RestCountryService));
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await DeserializeJsonAsync<IEnumerable<Country>>(responseStream) ??
                   throw new InvalidOperationException("Deserialization failed.");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Error fetching countries: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Retrieves information about all countries from the Rest Countries API.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="Country"/> objects.</returns>
    public async Task<IEnumerable<Country>> GetAll()
    {
        var url = $"{_options.Value.ApiVersion}/all";
        return await PerformCountryRequestAsync(url);
    }

    /// <summary>
    /// Retrieves information about a country by its name from the Rest Countries API.
    /// </summary>
    /// <param name="name">The name of the country.</param>
    /// <returns>The <see cref="Country"/> object representing the specified country.</returns>
    public async Task<IEnumerable<Country>> GetByName(string name)
    {
        var url =$"{_options.Value.ApiVersion}/name/{name}";
        return await PerformCountryRequestAsync(url);
    }

    /// <summary>
    /// Retrieves information about a country by its alpha code from the Rest Countries API.
    /// </summary>
    /// <param name="code">The alpha code of the country.</param>
    /// <returns>The <see cref="Country"/> object representing the specified country.</returns>
    public async Task<IEnumerable<Country>> GetByCode(string code)
    {
        var url = $"{_options.Value.ApiVersion}/alpha/{code}";
        return await PerformCountryRequestAsync(url);
    }

    /// <summary>
    /// Retrieves information about a country by its capital from the Rest Countries API.
    /// </summary>
    /// <param name="capital">The capital of the country.</param>
    /// <returns>The <see cref="Country"/> object representing the specified country.</returns>
    public async Task<IEnumerable<Country>> GetByCapital(string capital)
    {
        var url = $"{_options.Value.ApiVersion}/capital/{capital}";
        return await PerformCountryRequestAsync(url);
    }
}