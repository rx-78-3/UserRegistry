using Location.Contracts.Dtos;

namespace Location.Api.EndpointsMethods.GetProvinces.Models;

public record GetProvincesResult(ProvinceCountryDto[] Provinces);
