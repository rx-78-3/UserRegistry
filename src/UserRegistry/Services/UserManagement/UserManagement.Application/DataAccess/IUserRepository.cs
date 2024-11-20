using BuildingBlocks.Pagination;
using UserManagement.Domain.Models;

namespace UserManagement.Application.DataAccess;
public interface IUserRepository
{
    Task<PaginatedResult<User>> GetPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
    Task Create(User user, CancellationToken cancellationToken = default);
}
