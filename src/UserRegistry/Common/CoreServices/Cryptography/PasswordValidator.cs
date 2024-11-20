using CoreServices.Cryptography.Abstractions;

namespace CoreServices.Cryptography;

public class PasswordValidator : IPasswordValidator
{
    private readonly IPasswordHasher _passwordHasher;

    public PasswordValidator(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public bool ValidatePassword(string password, string passwordHash)
    {
        var salt = ExtractSalt(passwordHash);

        var computedPasswordHash = _passwordHasher.ComputeHash(password, salt);

        return passwordHash.Equals(computedPasswordHash);
    }

    private string ExtractSalt(string hashedPassword)
    {
        var parts = hashedPassword.Split(':');

        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid hashed password format.");
        }

        return parts[1];
    }
}
