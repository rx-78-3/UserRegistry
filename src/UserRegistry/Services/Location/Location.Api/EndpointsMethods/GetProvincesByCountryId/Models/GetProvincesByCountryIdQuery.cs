using BuildingBlocks.Cqrs;

namespace Location.Api.EndpointsMethods.GetProvincesByCountryId.Models;

public record GetProvincesByCountryIdQuery(Guid CountryId) : IQuery<GetProvincesByCountryIdResult>;
