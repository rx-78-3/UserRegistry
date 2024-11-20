using Identity.Api.Models;
using Identity.Api.Services.Abstractions;

namespace Identity.Api.EndpointMethods;

public static class LoginEndpoint
{
    public static void MapLogin(this WebApplication app)
    {
        app.MapPost("/login", async (
            IAuthService authService,
            ILogger<Program> logger,
            LoginRequest model,
            CancellationToken cancellationToken) =>
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                logger.LogWarning("The username or password is empty.");
                return Results.BadRequest();
            }

            var user = await authService.ValidateUserAsync(model.Email, model.Password, cancellationToken);

            if (user == null)
            {
                logger.LogWarning($"Failed login attempt made by user: {model.Email}.");
                return Results.Unauthorized();
            }

            var token = authService.GenerateJwtToken(user);

            logger.LogInformation($"User {model.Email} logged in successfully.");
            return Results.Ok(new LoginResponse(token)); // TODO: Add refresh token.
        })
        .WithName("Login")
        .WithSummary("Login")
        .WithDescription("Login")
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}
