using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Models;
using AviaSales.Shared.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;


namespace AviaSales.External.Services.Services;

/// <summary>
/// Service for interacting with the Flight Fare API.
/// </summary>
public class TimeTableService : ITimeTableService
{
    private readonly IHttpClientFactory _factory;
    private readonly ILogger<TimeTableService> _logger;
    private readonly IDistributedCache _cache;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeTableService"/> class.
    /// </summary>
    /// <param name="factory">Factory for creating HTTP clients.</param>
    /// <param name="logger">Logger for logging events.</param>
    /// <param name="cache">In memory cache</param>
    public TimeTableService(
        IHttpClientFactory factory, 
        ILogger<TimeTableService> logger, 
        IDistributedCache cache)
    {
        _factory = factory;
        _logger = logger;
        _cache = cache;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="date"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public async Task<OTA_AirDetailsRS?> SearchFlights(string from,string to,string date,byte count)
    {
        try
        {
            var key = string.Join("-", from, to, date, count);
            _cache.TryGetValue<OTA_AirDetailsRS>(key,out var cached);

            if (cached is not null) return cached;
            
            var client = _factory.CreateClient(nameof(TimeTableService));
            var response = await client.GetAsync($"TimeTable/{from}/{to}/{date}/?Results={count}");
            
            var content = await response.Content.ReadAsStringAsync();
            var result = await  content.DeserializeXml<OTA_AirDetailsRS>();
            
            await _cache.SetAsync(key, result);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "External service exception has occurred.");
            throw;
        }
    }
    
}
