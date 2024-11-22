using Location.Contracts.Dtos;

namespace Location.Contracts.GetProvincesByCountryId;

public record GetProvincesByCountryIdResponse(ProvinceSummaryDto[] Provinces);
