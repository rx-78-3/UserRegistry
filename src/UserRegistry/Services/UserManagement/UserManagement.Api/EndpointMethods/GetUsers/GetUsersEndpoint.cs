using BuildingBlocks.Pagination;
using Carter;
using MediatR;
using UserManagement.Application.Messages.Queries;

namespace UserManagement.Api.EndpointMethods.GetUsers;

public class GetUsersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async ([AsParameters] PaginationRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetUsersQuery(request);

            var result = await sender.Send(query, cancellationToken);

            var response = new GetUsersResponse(request, result);

            return Results.Ok(response);
        })
            .RequireAuthorization()
            .WithName("GetUsers")
            .WithSummary("GetUsers")
            .WithDescription("Get all users")
            .Produces<GetUsersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
