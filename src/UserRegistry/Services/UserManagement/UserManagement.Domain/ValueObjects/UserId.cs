using Domain.Base;

namespace UserManagement.Domain.ValueObjects;

public record UserId
{
    public Guid Value { get; }

    private UserId(Guid value) => Value = value;

    public static UserId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("OrderId cannot be empty.");
        }

        return new UserId(value);
    }
}
