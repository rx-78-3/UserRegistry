using MediatR;

namespace BuildingBlocks.Cqrs;

public interface IQuery<out TResponce>
    : IRequest<TResponce> 
    where TResponce : notnull
{
}
