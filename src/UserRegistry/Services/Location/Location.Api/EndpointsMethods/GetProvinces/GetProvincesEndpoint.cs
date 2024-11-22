using Carter;
using Location.Api.EndpointsMethods.GetProvinces.Models;
using Location.Contracts.GetProvinces;
using Location.Contracts.GetProvincesByCountryId;
using Mapster;
using MediatR;

namespace Location.Api.EndpointsMethods.GetProvinces;

public class GetProvincesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/provinces", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetProvincesQuery();

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetProvincesResponse>();
            return Results.Ok(response);
        })
            .RequireAuthorization()
            .WithName("GetProvinces")
            .WithSummary("GetProvinces")
            .WithDescription("Get all Provinces")
            .Produces<GetProvincesByCountryIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
