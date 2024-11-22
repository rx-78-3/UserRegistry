using Location.Contracts.Dtos;

namespace Location.Client.Abstractions;
public interface ILocationApiClient
{
    Task<Dictionary<Guid, ProvinceCountryDto>?> GetProvinceCountryDictionaryAsync(CancellationToken cancellationToken = default);

    Task<ProvinceDto?> GetProvinceAsync(Guid provinceId, CancellationToken cancellationToken = default);
}
