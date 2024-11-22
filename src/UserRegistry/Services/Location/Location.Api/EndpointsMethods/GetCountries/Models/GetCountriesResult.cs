using Location.Contracts.Dtos;

namespace Location.Api.EndpointsMethods.GetCountries.Models;

public record GetCountriesResult(CountryDto[] Countries);
