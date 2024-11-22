using Microsoft.EntityFrameworkCore;

namespace Location.Api.DataAccess.Extensions;

internal static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this LocationsDbContext dbContext)
    {
        await dbContext.Database.MigrateAsync();
        await SeedCountriesAsync(dbContext);
        await SeedProvincesAsync(dbContext);
    }

    public static async Task SeedCountriesAsync(LocationsDbContext dbContext)
    {
        if (!await dbContext.Countries.AnyAsync())
        {
            await dbContext.Countries.AddRangeAsync(InitialData.Countries);
            await dbContext.SaveChangesAsync();
        }
    }

    public static async Task SeedProvincesAsync(LocationsDbContext dbContext)
    {
        if (!await dbContext.Provinces.AnyAsync())
        {
            await dbContext.Provinces.AddRangeAsync(InitialData.Provinces);
            await dbContext.SaveChangesAsync();
        }
    }
}
