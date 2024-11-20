using System.Text;
using BuildingBlocks.Exceptions.Handler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BuildingBlocks;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddCors(options =>
            {
                options.AddPolicy(
                    "AllowSpecificOrigin",
                    policy => policy.WithOrigins(configuration["ServiceAddresses:FrontendUrl"]!)
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            })

            // Swagger.
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()

            .AddExceptionHandler<ServiceWideExceptionHandler>();

        return services;
    }

    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfigurationSection = configuration.GetSection("JwtSettings");
        var secretKey = jwtConfigurationSection["SecretKey"]!;
        var issuer = jwtConfigurationSection["Issuer"]!;
        var audience = jwtConfigurationSection["Audience"]!;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = key
            };
        });

        services.AddAuthorization();

        return services;
    }
}
