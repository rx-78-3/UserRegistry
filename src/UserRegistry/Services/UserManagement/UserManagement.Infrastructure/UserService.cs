using System.Data;
using BuildingBlocks.Pagination;
using Location.Client.Abstractions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Abstractions;
using UserManagement.DataAccess;
using UserManagement.Domain.Models;
using UserManagement.Domain.ValueObjects;
using UserManagement.Infrastructure.Extensions;

namespace UserManagement.Infrastructure;

public class UserService : IUserService
{
    private UsersDbContext _dbContext;
    private ILocationApiClient _locationApiClient;

    public UserService(UsersDbContext dbContext, ILocationApiClient locationApiClient)
    {
        _dbContext = dbContext;
        _locationApiClient = locationApiClient;
    }

    public async Task<PaginatedResult<User>> GetPaginated(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var provinceCountryIdsDictionary = await _locationApiClient.GetProvinceCountryIdsDictionaryAsync(cancellationToken);

        if (provinceCountryIdsDictionary == null)
        {
            throw new DataException("Failed to retrieve provinces from the location service.");
        }

        var totalCount = await _dbContext.Users.LongCountAsync(cancellationToken);
        var users = await _dbContext.Users
            .OrderBy(u => u.Email)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToArrayAsync();

        var usersWithLocations = users.Select(u =>
        {
            var countryId = provinceCountryIdsDictionary[u.ProvinceId];
            return u.ToDomain(countryId);
        });

        var usersResult = new PaginatedResult<User>(totalCount, usersWithLocations);
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

    public async Task<UserLocation?> GetAssociatedLocation(User user, CancellationToken cancellationToken = default)
    {
        var province = await _locationApiClient.GetProvinceAsync(user.Location.ProvinceId, cancellationToken);

        if (province?.Id == null || province?.CountryId == null)
        {
            return null;
        }    

        return UserLocation.Of(province.CountryId, province.Id);
    }
}
