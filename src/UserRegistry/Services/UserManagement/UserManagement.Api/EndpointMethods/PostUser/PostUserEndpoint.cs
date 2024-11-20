using Carter;
using MediatR;
using UserManagement.Application.Messages.Commands;


namespace UserManagement.Api.EndpointMethods.PostUser;

public class PostUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (PostUserRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateUserCommand(request.User);

            var user = await sender.Send(command, cancellationToken);

            return new PostUserResponse(user.Id);
        })
            .WithName("PostUser")
            .WithSummary("PostUser")
            .WithDescription("Create new user")
            .Produces<Guid>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict);
    }
}
