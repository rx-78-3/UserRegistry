using Location.Api;
using Location.Api.DataAccess;
using Location.Api.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddLocationApiServices(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseLocationApiServices();

// Initialize the database if in development mode.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<LocationsDbContext>();
    await dbContext.InitializeDatabaseAsync();
}

app.Run();
