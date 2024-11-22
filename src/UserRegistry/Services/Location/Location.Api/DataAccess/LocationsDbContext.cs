using System.Reflection;
using Location.Api.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Location.Api.DataAccess;

public class LocationsDbContext : DbContext
{
    public LocationsDbContext(DbContextOptions<LocationsDbContext> options) : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; } = default!;
    public DbSet<Province> Provinces { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
