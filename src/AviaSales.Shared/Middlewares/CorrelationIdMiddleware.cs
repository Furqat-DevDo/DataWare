using Microsoft.AspNetCore.Http;

namespace AviaSales.Shared.Middlewares;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers["CorrelationId"].FirstOrDefault() ?? Guid.NewGuid().ToString();
        
        context.Response.Headers.Add("CorrelationId", correlationId);
        
        context.Items["CorrelationId"] = correlationId;
        
        await _next(context);
    }
}
