using System.Reflection;
using BuildingBlocks;
using BuildingBlocks.PipelineBehaviors;
using Carter;
using Location.Api.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Location.Api;

internal static class DependencyInjection
{
    internal static IServiceCollection AddLocationApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:SqlServer"]!;

        services
            .AddCarter()
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            })
            .AddCommonApiServices(configuration)
            .AddAuthServices(configuration)
            .AddDbContext<LocationsDbContext>(options => 
                options.UseSqlServer(connectionString));
        return services;
    }

    internal static WebApplication UseLocationApiServices(this WebApplication app)
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Location API V1");
            });
        }

        return app;
    }
}
