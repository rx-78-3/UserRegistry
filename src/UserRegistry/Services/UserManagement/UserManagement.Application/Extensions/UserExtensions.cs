using UserManagement.Application.Dtos;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Extensions;

internal static class UserExtensions
{
    internal static UserDto[] ToDtoArray(this IEnumerable<User> users)
    {
        return users.Select(user => user.ToDto()).ToArray();
    }

    internal static UserDto ToDto(this User user)
    {
        return new UserDto(
            user.Id.Value,
            user.Email.Value,
            user.Location.Country.Name!,
            user.Location.Province.Name!);
    }
}
