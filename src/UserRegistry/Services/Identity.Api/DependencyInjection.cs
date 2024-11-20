using BuildingBlocks;
using CoreServices.Cryptography;
using CoreServices.Cryptography.Abstractions;
using Identity.Api.EndpointMethods;
using Identity.Api.Services;
using Identity.Api.Services.Abstractions;
using UserManagement.DataAccess;

namespace Identity.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:SqlServer"]!;

        services
            .AddCommonApiServices(configuration)
            .AddUserManagementDataAccessServices(configuration)
            .AddSingleton(configuration)
            .AddSingleton<IPasswordHasher, PasswordHasher>()
            .AddSingleton<IPasswordValidator, PasswordValidator>()
            .AddScoped<IAuthService, AuthService>();

        return services;
    }

    public static WebApplication UseIdentityApiServices(this WebApplication app)
    {
        app.UseCors("AllowSpecificOrigin");
        app.MapLogin();
        app.UseExceptionHandler(options => { });

        if (app.Environment.IsDevelopment())
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API V1");
            });
        }

        return app;
    }
}
