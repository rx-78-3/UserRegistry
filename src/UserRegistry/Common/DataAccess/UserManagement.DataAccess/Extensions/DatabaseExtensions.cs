using Microsoft.EntityFrameworkCore;

namespace UserManagement.DataAccess.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this UsersDbContext dbContext)
    {
        await dbContext.Database.MigrateAsync();
        await SeedUsersAsync(dbContext);
    }

    private static async Task SeedUsersAsync(UsersDbContext dbContext)
    {
        if (!await dbContext.Users.AnyAsync())
        {
            await dbContext.Users.AddRangeAsync(InitialData.Users);
            await dbContext.SaveChangesAsync();
        }
    }
}
