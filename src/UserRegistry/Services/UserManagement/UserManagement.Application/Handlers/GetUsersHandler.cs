using BuildingBlocks.Cqrs;
using BuildingBlocks.Pagination;
using UserManagement.Application.DataAccess;
using UserManagement.Application.Dtos;
using UserManagement.Application.Extensions;
using UserManagement.Application.Messages.Queries;

namespace UserManagement.Application.Handlers;

public class GetUsersHandler(IUserRepository userRepository) :
    IQueryHandler<GetUsersQuery, GetUsersResult>
{
    public async Task<GetUsersResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetPaginated(query.PageIndex, query.PageSize, cancellationToken);
        var usersDto = new PaginatedResult<UserDto>(users.TotalCount, users.Data.ToDtoArray());

        var usersResult = new GetUsersResult(usersDto);
        return usersResult;
    }
}
