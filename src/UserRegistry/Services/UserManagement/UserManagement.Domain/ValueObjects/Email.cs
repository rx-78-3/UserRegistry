using System.Text.RegularExpressions;
using Domain.Base;

namespace UserManagement.Domain.ValueObjects;

public record Email
{
    private static readonly Regex EmailRegex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);

    public string Value { get; }

    private Email(string value) => Value = value;

    public static Email Of(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (!EmailRegex.IsMatch(value))
        {
            throw new DomainException("Invalid email address");
        }

        return new Email(value);
    }
}
