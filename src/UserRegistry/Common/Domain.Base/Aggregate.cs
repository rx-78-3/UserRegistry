namespace Domain.Base;

public abstract class Aggregate<TId> : IAggregate<TId>
{
    public TId Id { get; protected set; }
}
