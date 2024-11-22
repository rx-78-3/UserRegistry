using Carter;
using Location.Api.EndpointsMethods.GetCountries.Models;
using Location.Contracts.GetCountries;
using Mapster;
using MediatR;

namespace Location.Api.EndpointsMethods.GetCountries;

public class GetCountriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/countries", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetCountriesQuery();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetCountriesResponse>();
            return Results.Ok(response);
        })
            .WithName("GetCountries")
            .WithSummary("GetCountries")
            .WithDescription("Get all Countries")
            .Produces<GetCountriesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
