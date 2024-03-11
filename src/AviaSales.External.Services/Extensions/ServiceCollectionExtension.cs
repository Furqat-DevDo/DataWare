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
    public static void AddFlightFareService(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.Configure<FlightFareOptions>(configuration.GetSection(nameof(FlightFareOptions)));

        
        var baseAddress = configuration.GetSection("FlightFareOptions:URL").Value
                          ?? throw new ArgumentException(nameof(FlightFareOptions));

        
        if (!Uri.TryCreate(baseAddress, UriKind.Absolute, out var uri))
        {
            throw new ArgumentException("Invalid base URL specified in configuration.");
        }

        
        services.AddTransient<IFlightFareService,FlightFareService>();

       
        services.AddHttpClient<FlightFareService>(
            nameof(FlightFareService), client =>
            {
                client.BaseAddress = uri;
            });
    }
}