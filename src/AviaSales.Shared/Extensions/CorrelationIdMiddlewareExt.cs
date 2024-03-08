using AviaSales.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace AviaSales.Shared.Extensions;

public static  class CorrelationIdMiddlewareExt
{
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CorrelationIdMiddleware>();
    }
}