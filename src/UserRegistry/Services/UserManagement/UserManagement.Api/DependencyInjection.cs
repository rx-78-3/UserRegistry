using BuildingBlocks;
using Carter;

namespace UserManagement.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddUserManagementApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddCarter()
            .AddCommonApiServices(configuration)
            .AddAuthServices(configuration);
        
        services.AddAuthorization();

        return services;
    }

    public static WebApplication UseUserManagementApiServices(this WebApplication app)
    {
        app.UseCors("AllowSpecificOrigin");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapCarter();
        app.UseExceptionHandler(options => { });

        if (app.Environment.IsDevelopment())
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API V1");
            });
        }

        return app;
    }
}
