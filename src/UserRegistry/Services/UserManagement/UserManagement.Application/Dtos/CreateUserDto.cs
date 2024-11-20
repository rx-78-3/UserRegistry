namespace UserManagement.Application.Dtos;

public record CreateUserDto(
    Guid Id,
    string Email,
    string Password,
    Guid ProvinceId);
