using Domain.Base;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Domain.Models;

public class User : Aggregate<UserId>
{
    public Email Email { get; private set; } = default!;
    public PasswordHash PasswordHash { get; private set; } = default!;
    public UserLocation Location { get; private set; } = default!;

    private User()
    {
    }

    public static User Create(UserId id, Email email, PasswordHash password, UserLocation location)
    {
        return new User
        {
            Id = id,
            Email = email,
            PasswordHash = password,
            Location = location
        };
    }
}