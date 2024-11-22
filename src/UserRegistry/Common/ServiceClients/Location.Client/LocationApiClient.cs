using System.Net;
using System.Net.Http.Json;
using Location.Client.Abstractions;
using Location.Contracts.Dtos;
using Location.Contracts.GetProvinces;
using Location.Contracts.GetProvince;

namespace Location.Client;

public class LocationApiClient : ILocationApiClient
{
    private readonly HttpClient _httpClient;

    public LocationApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Dictionary<Guid, ProvinceCountryDto>?> GetProvinceCountryDictionaryAsync(CancellationToken cancellationToken = default)
    {
        var url = $"/provinces";

        var response = await _httpClient.GetAsync(url, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var getCountryResponse = await response.Content.ReadFromJsonAsync<GetProvincesResponse>(cancellationToken);
            return getCountryResponse?.Provinces.ToDictionary(p => p.Id, p => p);
        }

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            throw new HttpRequestException($"Failed to get provinces. Status code: {response.StatusCode}.", e);
        }

        return null;
    }

    public async Task<ProvinceDto?> GetProvinceAsync(Guid provinceId, CancellationToken cancellationToken = default)
    {
        var url = $"/provinces/{provinceId}";

        var response = await _httpClient.GetAsync(url, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var getProvinceResponse = await response.Content.ReadFromJsonAsync<GetProvinceResponse>(cancellationToken);
            return getProvinceResponse?.Province;
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            throw new HttpRequestException($"Failed to get province. Status code: {response.StatusCode}.", e);
        }

        return null;
    }
}
