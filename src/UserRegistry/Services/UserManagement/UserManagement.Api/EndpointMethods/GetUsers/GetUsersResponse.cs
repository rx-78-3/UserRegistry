using BuildingBlocks.Pagination;
using UserManagement.Application.Dtos;

namespace UserManagement.Api.EndpointMethods.GetUsers;

public record GetUsersResponse : PaginatedResponse<UserDto>
{
    public GetUsersResponse(PaginationRequest request, PaginatedResult<UserDto> result) : base(request, result)
    {
    }
}
