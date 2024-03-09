using System.Reflection;
using AviaSales.Infrastructure.Persistance;
using AviaSales.Shared.Extensions;
using AviaSales.Shared.Utilities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AviaSales.API.Extensions;

public static class ServiceCollectionExtension
{
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
    }
    
    public static void AddExtensions(this IServiceCollection services,IHostBuilder hostBuilder)
    {
        // URLs in lowercase
        services.AddRouting(r =>
        {
            r.LowercaseUrls = true;
        });
        
        // Adding serilog
        hostBuilder.UseSerilog(SerilogHelper.Configure);
        
        // Adding swagger
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