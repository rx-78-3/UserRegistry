using BuildingBlocks.Pagination;
using UserManagement.Domain.Models;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Application.Abstractions;
public interface IUserService
{
    Task<PaginatedResult<User>> GetPaginated(int pageIndex, int pageSize, CancellationToken cancellationToken = default);
    Task Create(User user, CancellationToken cancellationToken = default);
    Task<UserLocation?> GetAssociatedLocation(User user, CancellationToken cancellationToken = default);
}
