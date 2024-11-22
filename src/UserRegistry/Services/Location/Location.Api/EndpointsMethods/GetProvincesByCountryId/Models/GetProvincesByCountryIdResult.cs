using Location.Contracts.Dtos;

namespace Location.Api.EndpointsMethods.GetProvincesByCountryId.Models;

public record GetProvincesByCountryIdResult(ProvinceSummaryDto[] Provinces);
