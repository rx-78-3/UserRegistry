using Location.Contracts.Dtos;

namespace Location.Client.Abstractions;
public interface ILocationApiClient
{
    Task<Dictionary<Guid, Guid>?> GetProvinceCountryIdsDictionaryAsync(CancellationToken cancellationToken = default);

    Task<ProvinceDto?> GetProvinceAsync(Guid provinceId, CancellationToken cancellationToken = default);
}
