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

    public async Task<Dictionary<Guid, ProvinceCountryDto>?> GetProvinceCountryDictionaryAsync(CancellationToken cancellationToken = default)
    {
        var cacheKey = "ProvinceCountryIdsDictionary";
        if (_cache.TryGetValue(cacheKey, out Dictionary<Guid, ProvinceCountryDto>? cachedProvinces))
        {
            return cachedProvinces;
        }

        var provinceCountryDictionary = await _innerApiClient.GetProvinceCountryDictionaryAsync(cancellationToken);
        if (provinceCountryDictionary != null)
        {
            _cache.Set(cacheKey, provinceCountryDictionary, CacheDuration);
        }
        return provinceCountryDictionary;
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
