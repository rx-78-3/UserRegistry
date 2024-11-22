namespace UserManagement.Application.Dtos;

public record CreateUserDto(
    string Email,
    string Password,
    Guid CountryId,
    Guid ProvinceId);
