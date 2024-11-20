using UserManagement.Domain.ValueObjects;
using UserDb = UserManagement.DataAccess.Models.User;
using UserDomain = UserManagement.Domain.Models.User;

namespace UserManagement.Infrastructure.Extensions;

internal static class UserExtensions
{
    internal static UserDomain[] ToDomainArray(this IEnumerable<UserDb> userDbs)
    {
        return userDbs.Select(userDb => userDb.ToDomain()).ToArray();
    }

    internal static UserDomain ToDomain(this UserDb userDb)
    {
        return UserDomain.Create(
            UserId.Of(userDb.Id),
            Email.Of(userDb.Email),
            PasswordHash.Of(userDb.PasswordHash),
            ProvinceId.Of(userDb.ProvinceId));
    }

    internal static UserDb ToDb(this UserDomain userDomain)
    {
        return new UserDb
        {
            Id = userDomain.Id.Value,
            Email = userDomain.Email.Value,
            PasswordHash = userDomain.PasswordHash.Value,
            ProvinceId = userDomain.ProvinceId.Value
        };
    }
}
