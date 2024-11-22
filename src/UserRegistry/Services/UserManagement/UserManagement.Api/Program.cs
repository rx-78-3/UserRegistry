using UserManagement.Api;
using UserManagement.Application;
using UserManagement.DataAccess;
using UserManagement.DataAccess.Extensions;
using UserManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services
    .AddUserManagementInfrastructureServices(configuration)
    .AddUserManagementApplicationServices(configuration)
    .AddUserManagementApiServices(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseUserManagementApiServices();

// Initialize the database if in development mode.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
    await dbContext.InitializeDatabaseAsync();
}

app.Run();