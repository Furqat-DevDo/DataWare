using System.Reflection;
using AviaSales.Admin.UseCases.Airline;
using AviaSales.Admin.UseCases.Airport;
using AviaSales.Admin.UseCases.Booking;
using AviaSales.Admin.UseCases.Country;
using AviaSales.Admin.UseCases.Flight;
using AviaSales.Admin.UseCases.Passenger;
using AviaSales.External.Services.Extensions;
using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Services;
using AviaSales.Persistence;
using AviaSales.Shared.Extensions;
using AviaSales.Shared.Utilities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AviaSales.Admin.Api.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adding DbContext and managers here.
    /// </summary>
    /// <param name="services">IServiceCollection interface.</param>
    /// <param name="configuration">IConfiguration interface.</param>
    /// <exception cref="ArgumentNullException">When appsettings is not setted.</exception>
    public static void AddServices(this IServiceCollection services,IConfiguration configuration)
    {
        // Adding Entity Managers.
        services.AddScoped<AirlineManager>()
            .AddScoped<CountryManager>()
            .AddScoped<AirportManager>()
            .AddScoped<FlightManager>()
            .AddScoped<BookingManager>()
            .AddScoped<PassengerManager>();

        services.AddScoped<IFakeService, FakeService>();
        services.AddCountryService(configuration);
        services.AddTimeTableService(configuration);
    }

    /// <summary>
    /// Adding DbContext and settings.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddDb(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<AviaSalesDb>(options =>
        {
            var connection = configuration.GetConnectionString(nameof(AviaSalesDb))
                             ?? throw new ArgumentNullException(nameof(AviaSalesDb));
            
            options.UseNpgsql(connection, o =>
            {
                o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });
    }
    
    /// <summary>
    /// Will add all validators.
    /// </summary>
    /// <param name="services">IServiceCollection interface.</param>
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AirlineValidator>();
    } 
    
    /// <summary>
    /// Will add extension methods from shared project.
    /// </summary>
    /// <param name="services">IServiceCollection interface.</param>
    /// <param name="hostBuilder">IHostBuilder interface.</param>
    public static void AddExtensions(this IServiceCollection services,IHostBuilder hostBuilder)
    {
        // URLs in lowercase
        services.AddRouting(r =>
        {
            r.LowercaseUrls = true;
        });
        
        // Adding serilog
        hostBuilder.UseSerilog(SerilogHelper.Configure);
        
        // Adding swagger and doc generating options.
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(s =>
        {
            s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            
            s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{typeof(AirlineDto).Assembly.GetName().Name}.xml"));
            
            s.AddTokenAuth();
        });

        services.AddDistributedMemoryCache();
    }
}