using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AviaSales.Shared.Extensions;

/// <summary>
/// Extension class providing simplified JWT-based authentication configuration for ASP.NET Core applications.
/// </summary>
public static class AuthExtension
{
    /// <summary>
    /// Adds JWT authentication to the specified service collection with the provided configuration.
    /// </summary>
    /// <param name="services">The service collection to extend.</param>
    /// <param name="configuration">The configuration containing JWT-related settings.</param>
    /// <returns>An AuthenticationBuilder for additional configuration.</returns>
    /// <exception cref="ArgumentNullException">Thrown when essential JWT configuration parameters are null or empty.</exception>
    public static AuthenticationBuilder AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: Handle this situation more elegantly!!!
        var securityKey = configuration["Jwt:SecurityKey"] 
                          ?? throw new ArgumentNullException("Jwt:SecurityKey", "Security key cannot be null or empty.");
        
        var audiences = configuration.GetSection("Jwt:Audiences").Get<string[]>() 
                        ?? throw new ArgumentNullException("Jwt:Audiences", "Audiences cannot be null or empty.");
        
        var issuer = configuration["Jwt:Issuer"] 
                     ?? throw new ArgumentNullException("Jwt:Issuer", "Issuer cannot be null or empty.");

        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {   
                    ValidAudiences = audiences,
                    ValidIssuer = issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey)),
                    ClockSkew = TimeSpan.Zero,
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role,
                };
            });
    }
}
