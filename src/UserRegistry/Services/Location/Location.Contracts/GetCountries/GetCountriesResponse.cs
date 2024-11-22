using Location.Contracts.Dtos;

namespace Location.Contracts.GetCountries;

public record GetCountriesResponse(CountryDto[] Countries);
