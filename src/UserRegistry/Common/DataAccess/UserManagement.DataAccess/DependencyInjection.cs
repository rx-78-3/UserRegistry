using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserManagement.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddUserManagementDataAccessServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:SqlServer"]!;

        services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}
