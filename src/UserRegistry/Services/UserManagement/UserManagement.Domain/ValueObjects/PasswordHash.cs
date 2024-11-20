namespace UserManagement.Domain.ValueObjects;

public record PasswordHash
{
    public string Value { get; }

    private PasswordHash(string value) => Value = value;

    public static PasswordHash Of(string passwordHash)
    {
        ArgumentNullException.ThrowIfNull(passwordHash);

        return new(passwordHash);
    }
}
