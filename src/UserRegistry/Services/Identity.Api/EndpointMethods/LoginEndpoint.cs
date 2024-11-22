using Identity.Api.Models;
using Identity.Api.Services.Abstractions;

namespace Identity.Api.EndpointMethods;

public static class LoginEndpoint
{
    public static void MapLogin(this WebApplication app)
    {
        app.MapPost("/login", async (
            LoginRequest request,
            IAuthService authService,
            ILogger<Program> logger,
            CancellationToken cancellationToken) =>
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                logger.LogWarning("The username or password is empty.");
                return Results.BadRequest();
            }

            var user = await authService.ValidateUserAsync(request.Email, request.Password, cancellationToken);

            if (user == null)
            {
                logger.LogWarning($"Failed login attempt made by user: {request.Email}.");
                return Results.Unauthorized();
            }

            var token = authService.GenerateJwtToken(user);

            logger.LogInformation($"User {request.Email} logged in successfully.");
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
