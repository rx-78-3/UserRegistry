namespace UserManagement.Application.Dtos;

public record UserDto(
    Guid Id,
    string Email,
    string CountryName,
    string ProvinceName);
