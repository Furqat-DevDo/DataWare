using System.Reflection;
using AviaSales.External.Services.Extensions;
using AviaSales.External.Services.Interfaces;
using AviaSales.External.Services.Services;
using AviaSales.UseCases.Booking;
using AviaSales.UseCases.Flight;
using AviaSales.Persistence;
using AviaSales.Shared.Extensions;
using AviaSales.Shared.Utilities;
using AviaSales.UseCases.Airline;
using AviaSales.UseCases.Airport;
using AviaSales.UseCases.Country;
using AviaSales.UseCases.Passenger;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AviaSales.Api.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adding managers and services here.
    /// </summary>
    /// <param name="services">IServiceCollection interface.</param>
    /// <param name="configuration">IConfiguration interface.</param>
    public static void AddServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<FlightManager>()
            .AddScoped<PassengerManager>()
            .AddScoped<AirportManager>()
            .AddScoped<AirlineManager>()
            .AddScoped<CountryManager>()
            .AddScoped<BookingManager>();
        
        services.AddScoped<IFakeService, FakeService>();
        
        services.AddCountryService(configuration);
        services.AddTimeTableService(configuration);
        
        services.AddDistributedMemoryCache();
    }

    /// <summary>
    /// Will Add DbContext and it's settings.
    /// </summary>
    /// <param name="services">IServiceCollection interface.</param>
    /// <param name="configuration">IConfiguration interface.</param>
    /// <exception cref="ArgumentNullException">When appsettings is not setted.</exception>
    public static void AddDb(this IServiceCollection services, IConfiguration configuration)
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
        services.AddValidatorsFromAssemblyContaining<BookingValidator>();
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
                $"{typeof(FlightDto).Assembly.GetName().Name}.xml"));
            
            s.AddTokenAuth();
        });
    }
}