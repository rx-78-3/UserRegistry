namespace UserManagement.Application.Dtos;

public record UserDto(
    Guid Id,
    string Email,
    Guid CountryId,
    Guid ProvinceId);
