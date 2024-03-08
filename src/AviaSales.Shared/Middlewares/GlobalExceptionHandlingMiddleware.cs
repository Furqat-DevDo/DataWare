using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AviaSales.Shared.Middlewares;

/// <summary>
/// Middleware for global exception handling in an ASP.NET Core application. 
/// Captures unhandled exceptions, logs them, and returns a JSON response with error details.
/// </summary>
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the GlobalExceptionHandlingMiddleware class.
    /// </summary>
    /// <param name="logger">The logger for capturing exception details.</param>
    /// <param name="next">The next middleware in the request processing pipeline.</param>
    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }
    
    /// <summary>
    /// Invokes the middleware to handle exceptions during the HTTP request processing.
    /// </summary>
    /// <param name="context">Represents the current HTTP request context.</param>
    /// <returns>A task representing the asynchronous processing of the request.</returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }
    
    /// <summary>
    /// Handles unhandled exceptions by serializing the error details into a JSON response.
    /// </summary>
    /// <param name="context">Represents the current HTTP request context.</param>
    /// <param name="exception">The unhandled exception that occurred.</param>
    /// <returns>A task representing the asynchronous handling of the exception.</returns>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var result = JsonSerializer.Serialize(
            new ProblemDetails 
            {
              Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
              Title = "An error occurred while processing your request.",
              Status = (int)HttpStatusCode.InternalServerError,
              Detail = exception.Message
            });
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        
        return context.Response.WriteAsync(result);
    }
}
