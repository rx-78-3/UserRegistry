using Identity.Api;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddIdentityApiServices(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseIdentityApiServices();

app.Run();