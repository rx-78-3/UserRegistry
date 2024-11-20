using BuildingBlocks.Pagination;
using UserManagement.Application.Dtos;

namespace UserManagement.Application.Messages.Queries;

public record GetUsersResult : PaginatedResult<UserDto>
{
    public GetUsersResult(PaginatedResult<UserDto> origin) : base(origin)
    {
    }
}
