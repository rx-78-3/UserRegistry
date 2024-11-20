namespace Domain.Base;

public interface IAggregate<TId>
{
    TId Id { get; }
}
