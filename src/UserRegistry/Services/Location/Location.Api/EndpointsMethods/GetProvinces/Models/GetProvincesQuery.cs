using BuildingBlocks.Cqrs;

namespace Location.Api.EndpointsMethods.GetProvinces.Models;

public record GetProvincesQuery() : IQuery<GetProvincesResult>;
