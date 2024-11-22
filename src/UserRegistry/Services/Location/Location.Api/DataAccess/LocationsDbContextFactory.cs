using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Location.Api.DataAccess;

public class LocationsDbContextFactory : IDesignTimeDbContextFactory<LocationsDbContext>
{
    public LocationsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LocationsDbContext>();
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__SqlServer");

        optionsBuilder.UseSqlServer(connectionString);

        return new LocationsDbContext(optionsBuilder.Options);
    }
}
