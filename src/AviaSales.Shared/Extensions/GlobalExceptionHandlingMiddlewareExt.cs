using AviaSales.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace AviaSales.Shared.Extensions;

public static class GlobalExceptionHandlingMiddlewareExt
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}