using Domain.Base;

namespace UserManagement.Domain.ValueObjects;
public record Country
{
    public Guid Id { get; }
    public string? Name { get; }

    private Country(Guid id) => Id = id;

    private Country(Guid id, string name) => (Id, Name) = (id, name);

    public static Country Of(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("Country Id cannot be empty.");
        }

        return new Country(id);
    }

    public static Country Of(Guid id, string name)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("Country Id cannot be empty.");
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Country name cannot be empty.");
        }

        return new Country(id, name);
    }
}
