using AviaSales.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace AviaSales.Shared.Extensions;

public static class GlobalExceptionHandlingMiddlewareExt
{ 
    /// <summary>
    /// A method to add global exception handling middleware to the application request pipeline.
    /// </summary>
    /// <param name="builder"></param>
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}