using Microsoft.AspNetCore.Http;

namespace AviaSales.Shared.Middlewares;

/// <summary>
/// The purpose of this middleware is to handle correlation IDs for incoming requests.
/// </summary>
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Asynchronously invokes the specified function with the provided HttpContext.
    /// Sets the correlationId in the request headers if not present, adds the correlationId to the response headers.
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers["CorrelationId"].FirstOrDefault() ?? Guid.NewGuid().ToString();
        
        context.Response.Headers.Add("CorrelationId", correlationId);
        
        context.Items["CorrelationId"] = correlationId;
        
        await _next(context);
    }
}
