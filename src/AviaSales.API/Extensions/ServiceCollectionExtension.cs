using System.Reflection;
using AviaSales.Infrastructure.Dtos;
using AviaSales.Infrastructure.Managers;
using AviaSales.Infrastructure.Persistance;
using AviaSales.Infrastructure.Validations;
using AviaSales.Shared.Extensions;
using AviaSales.Shared.Utilities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AviaSales.API.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adding DbContext and managers here.
    /// </summary>
    /// <param name="services">IServiceCollection interface.</param>
    /// <param name="configuration">IConfiguration interface.</param>
    /// <exception cref="ArgumentNullException">When appsettings is not setted.</exception>
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        // Adding Dbcontext and Split query behavior
        services.AddDbContext<AviaSalesDb>(options =>
        {
            var connection = configuration.GetConnectionString(nameof(AviaSalesDb))
                             ?? throw new ArgumentNullException(nameof(AviaSalesDb));
            
            options.UseNpgsql(connection, o =>
            {
                o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });
        
        // Adding Entity Managers.
        services.AddScoped<AirlineManager>()
            .AddScoped<AirportManager>()
            .AddScoped<FlightManager>()
            .AddScoped<BookingManager>()
            .AddScoped<PassengerManager>();
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
    }
}