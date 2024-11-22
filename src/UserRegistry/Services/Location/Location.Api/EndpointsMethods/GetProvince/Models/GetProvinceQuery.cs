using BuildingBlocks.Cqrs;

namespace Location.Api.EndpointsMethods.GetProvince.Models;

public record GetProvinceQuery(Guid ProvinceId) : IQuery<GetProvinceResult>;
