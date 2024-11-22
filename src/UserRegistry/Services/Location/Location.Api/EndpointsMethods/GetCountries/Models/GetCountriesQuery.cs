using BuildingBlocks.Cqrs;

namespace Location.Api.EndpointsMethods.GetCountries.Models;

public record GetCountriesQuery() : IQuery<GetCountriesResult>;
