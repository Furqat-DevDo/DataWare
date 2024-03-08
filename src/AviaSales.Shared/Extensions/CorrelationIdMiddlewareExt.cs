using AviaSales.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace AviaSales.Shared.Extensions;

public static  class CorrelationIdMiddlewareExt
{
    /// <summary>
    /// Adds a correlation ID middleware to the application's request pipeline.
    /// </summary>
    /// <param name="builder"></param>
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CorrelationIdMiddleware>();
    }
}