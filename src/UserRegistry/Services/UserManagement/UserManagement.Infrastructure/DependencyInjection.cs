using System.Net.Http.Headers;
using BuildingBlocks.Extensions.Http;
using Location.Client;
using Location.Client.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Abstractions;
using UserManagement.DataAccess;

namespace UserManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUserManagementInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var locationClientServiceUrl = configuration["LocationClientSettings:ServiceUrl"]!;
        var locationServiceCacheDurationInMinutes = long.Parse(configuration["LocationClientSettings:CacheDurationInMinutes"]!);

        services
            .AddUserManagementDataAccessServices(configuration)
            .AddMemoryCache()
            .AddHttpContextAccessor()
            .AddScoped<IUserService, UserService>()
            .AddTransient<ILocationApiClient>(provider =>
            {
                var innerClient = provider.GetRequiredService<LocationApiClient>();
                var cache = provider.GetRequiredService<IMemoryCache>();
                return new CacheableLocationApiClient(innerClient, cache, locationServiceCacheDurationInMinutes);
            })
            .AddHttpClient<LocationApiClient>((serviceProvider, client) =>
            {
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                var accessToken = httpContextAccessor.HttpContext?.ExtractTokenFromContext();

                if (accessToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }

                client.BaseAddress = new Uri(locationClientServiceUrl);
            });

        return services;
    }
}
