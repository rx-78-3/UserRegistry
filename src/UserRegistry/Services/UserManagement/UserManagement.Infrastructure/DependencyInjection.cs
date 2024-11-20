using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.DataAccess;
using UserManagement.DataAccess;
using UserManagement.Infrastructure.DataAccess;

namespace UserManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUserManagementInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddUserManagementDataAccessServices(configuration)
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
