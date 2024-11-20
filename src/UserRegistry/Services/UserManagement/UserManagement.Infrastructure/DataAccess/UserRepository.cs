using System.Data;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.DataAccess;
using UserManagement.DataAccess;
using UserManagement.Domain.Models;
using UserManagement.Infrastructure.Extensions;

namespace UserManagement.Infrastructure.DataAccess;

public class UserRepository : IUserRepository
{
    private UsersDbContext _dbContext;

    public UserRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginatedResult<User>> GetPaginated(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await _dbContext.Users.LongCountAsync(cancellationToken);
        var users = await _dbContext.Users
            .OrderBy(u => u.Email)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToArrayAsync();

        var usersResult = new PaginatedResult<User>(totalCount, users.ToDomainArray());
        return usersResult;
    }

    public async Task Create(User user, CancellationToken cancellationToken = default)
    {
        var existingUser = await _dbContext.Users
            .SingleOrDefaultAsync(u => u.Email == user.Email.Value, cancellationToken);

        if (existingUser != null)
        {
            throw new DuplicateNameException($"A user with the email '{user.Email.Value}' already exists.");
        }

        _dbContext.Users.Add(user.ToDb());
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
