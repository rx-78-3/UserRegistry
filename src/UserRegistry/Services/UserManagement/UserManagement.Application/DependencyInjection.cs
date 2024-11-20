using System.Reflection;
using BuildingBlocks.PipelineBehaviors;
using CoreServices.Cryptography;
using CoreServices.Cryptography.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserManagement.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddUserManagementApplicationServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            })
            .AddMemoryCache()
            .AddSingleton<ISaltGenerator, SaltGenerator>()
            .AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
