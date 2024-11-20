using BuildingBlocks.Cqrs;
using BuildingBlocks.Pagination;

namespace UserManagement.Application.Messages.Queries;

public record GetUsersQuery : PaginationRequest, IQuery<GetUsersResult>
{
    public GetUsersQuery(PaginationRequest original) : base(original)
    {
    }
}
