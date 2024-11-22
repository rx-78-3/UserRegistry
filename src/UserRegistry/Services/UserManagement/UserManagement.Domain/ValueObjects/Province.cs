using Domain.Base;

namespace UserManagement.Domain.ValueObjects;

public record Province
{
    public Guid Id { get; }
    public string? Name { get; }

    private Province(Guid id) => this.Id = id;

    private Province(Guid id, string name) => (Id, Name) = (id, name);

    public static Province Of(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("Province Id cannot be empty.");
        }

        return new Province(id);
    }

    public static Province Of(Guid value, string name)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("Province Id cannot be empty.");
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Province name cannot be empty.");
        }

        return new Province(value, name);
    }
}
