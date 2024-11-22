namespace Location.Contracts.Dtos;

public record ProvinceCountryDto(Guid Id, string Name, Guid CountryId, CountryDto Country);
