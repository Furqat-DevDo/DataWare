using System.Reflection;
using AviaSales.Shared.Extensions;
using AviaSales.Shared.Utilities;
using Serilog;

namespace AviaSales.API;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        
    }
    
    public static void AddExtensions(this IServiceCollection services,IHostBuilder hostBuilder)
    {
        // adding serilog
        hostBuilder.UseSerilog(SerilogHelper.Configure);
        
        // adding swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(s =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            s.IncludeXmlComments(xmlPath);
            s.AddTokenAuth();
        });
    }
}