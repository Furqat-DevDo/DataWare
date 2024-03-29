﻿using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Models;
using AviaSales.External.Services.Options;
using AviaSales.Shared.Extensions;
using Microsoft.Extensions.Caching.Distributed;
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
    private readonly IDistributedCache _cache;
    private readonly IOptions<CacheOptions> _cacheOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="RestCountryService"/> class.
    /// </summary>
    /// <param name="logger">The logger for logging events.</param>
    /// <param name="options">The options for configuring the Rest Country client.</param>
    /// <param name="factory">Http Client factory.</param>
    /// <param name="cache">In memory cache</param>
    /// <param name="cacheOptions">Cache options like sliding expiration time or absolute one.</param>
    public RestCountryService(ILogger<RestCountryService> logger, 
        IOptions<RestCountryOptions> options,
        IHttpClientFactory factory, 
        IDistributedCache cache, 
        IOptions<CacheOptions> cacheOptions)
    {
        _logger = logger;
        _options = options;
        _factory = factory;
        _cache = cache;
        _cacheOptions = cacheOptions;
    }

    /// <summary>
    /// Performs an HTTP request for a list of countries based on the provided URL.
    /// </summary>
    /// <param name="url">The URL for the countries request.</param>
    /// <returns>The deserialized list of <see cref="RestCountry"/> objects.</returns>
    private async Task<IEnumerable<RestCountry>> PerformCountryRequestAsync(string url,string key)
    {
        try
        {
            var client = _factory.CreateClient(nameof(RestCountryService));
            
            _cache.TryGetValue<IEnumerable<RestCountry>>(key, out var response);

            if (response is not null) return response;
            
            var result = await client.GetJsonAsync<IEnumerable<RestCountry>>(url);

            if (result is not null)
            {
                await _cache.SetAsync(key, result,
                    new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromMinutes(_cacheOptions.Value.SlidingTimeMinute),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(_cacheOptions.Value.AbsoluteTimeDay)
                    });
                return result;
            }
            
            return Enumerable.Empty<RestCountry>();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex,"Error fetching countries.");
            throw;
        }
    }

    /// <summary>
    /// Retrieves information about all countries from the Rest Countries API.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="RestCountry"/> objects.</returns>
    public async Task<IEnumerable<RestCountry>> GetAll()
    {
        var url = $"{_options.Value.ApiVersion}/all";
        return await PerformCountryRequestAsync(url,"all");
    }

    /// <summary>
    /// Retrieves information about a country by its name from the Rest Countries API.
    /// </summary>
    /// <param name="name">The name of the country.</param>
    /// <returns>The <see cref="RestCountry"/> object representing the specified country.</returns>
    public async Task<IEnumerable<RestCountry>> GetByName(string name)
    {
        var url =$"{_options.Value.ApiVersion}/name/{name}";
        return await PerformCountryRequestAsync(url,name);
    }

    /// <summary>
    /// Retrieves information about a country by its alpha code from the Rest Countries API.
    /// </summary>
    /// <param name="code">The alpha code of the country.</param>
    /// <returns>The <see cref="RestCountry"/> object representing the specified country.</returns>
    public async Task<IEnumerable<RestCountry>> GetByCode(string code)
    {
        var url = $"{_options.Value.ApiVersion}/alpha/{code}";
        return await PerformCountryRequestAsync(url,code);
    }

    /// <summary>
    /// Retrieves information about a country by its capital from the Rest Countries API.
    /// </summary>
    /// <param name="capital">The capital of the country.</param>
    /// <returns>The <see cref="RestCountry"/> object representing the specified country.</returns>
    public async Task<IEnumerable<RestCountry>> GetByCapital(string capital)
    {
        var url = $"{_options.Value.ApiVersion}/capital/{capital}";
        return await PerformCountryRequestAsync(url,capital);
    }
}
