using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Options;
using AviaSales.External.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AviaSales.External.Services.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adds configuration and services for the Rest Country API to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to configure.</param>
    /// <param name="configuration">The configuration containing RestCountryOptions.</param>
    /// <exception cref="ArgumentException">Thrown when an invalid base URL is specified in the configuration.</exception>
    public static void AddCountryService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RestCountryOptions>(configuration.GetSection(nameof(RestCountryOptions)));
        services.Configure<CacheOptions>(configuration.GetSection(nameof(CacheOptions)));
        
        var baseAddress = configuration.GetSection("RestCountryOptions:URL").Value
                          ?? throw new ArgumentException(nameof(RestCountryOptions));
        
        if (!Uri.TryCreate(baseAddress, UriKind.Absolute, out var uri))
        {
            throw new ArgumentException("Invalid base URL specified in configuration.");
        }
        
        services.AddTransient<ICountryService, RestCountryService>();
        
        services.AddHttpClient<RestCountryService>(
            nameof(RestCountryService), client =>
            {
                client.BaseAddress = uri;
            });
    }
    
    /// <summary>
    /// Adds configuration and services for the Flight Fare API to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to configure.</param>
    /// <param name="configuration">The configuration containing FlightFareOptions.</param>
    /// <exception cref="ArgumentException">Thrown when an invalid base URL is specified in the configuration.</exception>
    public static void AddTimeTableService(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.Configure<TimeTableOptions>(configuration.GetSection(nameof(TimeTableOptions)));
        services.Configure<CacheOptions>(configuration.GetSection(nameof(CacheOptions)));
        
        var baseAddress = configuration.GetSection("TimeTableOptions:URL").Value
                          ?? throw new ArgumentException(nameof(TimeTableOptions));
        
        var apiKey = configuration.GetSection("TimeTableOptions:ApiKey").Value
                     ?? throw new ArgumentException(nameof(TimeTableOptions));
        
        var apiHost = configuration.GetSection("TimeTableOptions:ApiHost").Value
                     ?? throw new ArgumentException(nameof(TimeTableOptions));
        
        if (!Uri.TryCreate(baseAddress, UriKind.Absolute, out var uri))
        {
            throw new ArgumentException("Invalid base URL specified in configuration.");
        }

        
        services.AddTransient<ITimeTableService,TimeTableService>();

       
        services.AddHttpClient<TimeTableService>(
            nameof(TimeTableService), client =>
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", apiHost);
            });
    }
}