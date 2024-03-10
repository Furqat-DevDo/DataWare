using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Options;
using AviaSales.External.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AviaSales.External.Services.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddCountryService(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure Rest Country Options.
        services.Configure<RestCountryOptions>(configuration.GetSection(nameof(RestCountryOptions)));

        // Retrieve the base URL from configuration
        var baseAddress = configuration.GetSection("RestCountryOptions:URL").Value
                          ?? throw new ArgumentException(nameof(RestCountryOptions));

        // Ensure the base URL is a valid URI
        if (!Uri.TryCreate(baseAddress, UriKind.Absolute, out var uri))
        {
            throw new ArgumentException("Invalid base URL specified in configuration.");
        }

        // Register RestCountryService for ICountryService
        services.AddTransient<ICountryService, RestCountryService>();

        // Configure HttpClient with base address and register RestCountryService for HttpClient
        services.AddHttpClient<RestCountryService>(
            nameof(RestCountryService), client =>
            {
                client.BaseAddress = uri;
            });
    }
}