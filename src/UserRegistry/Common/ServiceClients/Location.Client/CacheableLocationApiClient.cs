using Location.Client.Abstractions;
using Location.Contracts.Dtos;
using Microsoft.Extensions.Caching.Memory;

namespace Location.Client;
public class CacheableLocationApiClient : ILocationApiClient
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

    private readonly ILocationApiClient _innerApiClient;
    private readonly IMemoryCache _cache;

    public CacheableLocationApiClient(ILocationApiClient innerApiClient, IMemoryCache cache, long cacheDurationInMinutes)
    {
        _innerApiClient = innerApiClient;
        _cache = cache;
    }

    public async Task<Dictionary<Guid, Guid>?> GetProvinceCountryIdsDictionaryAsync(CancellationToken cancellationToken = default)
    {
        var cacheKey = "ProvinceCountryIdsDictionary";
        if (_cache.TryGetValue(cacheKey, out Dictionary<Guid, Guid>? cachedProvinces))
        {
            return cachedProvinces;
        }

        var provinceCountryIdsDictionary = await _innerApiClient.GetProvinceCountryIdsDictionaryAsync(cancellationToken);
        if (provinceCountryIdsDictionary != null)
        {
            _cache.Set(cacheKey, provinceCountryIdsDictionary, CacheDuration);
        }
        return provinceCountryIdsDictionary;
    }

    public async Task<ProvinceDto?> GetProvinceAsync(Guid provinceId, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"Province_{provinceId}";

        if (_cache.TryGetValue(cacheKey, out ProvinceDto? cachedProvince))
        {
            return cachedProvince;
        }

        var province = await _innerApiClient.GetProvinceAsync(provinceId, cancellationToken);
        if (province != null)
        {
            _cache.Set(cacheKey, province, CacheDuration);
        }

        return province;
    }
}
