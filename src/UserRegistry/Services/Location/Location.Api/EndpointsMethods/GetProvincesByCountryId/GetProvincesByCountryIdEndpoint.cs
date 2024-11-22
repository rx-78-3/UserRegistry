using Carter;
using Location.Api.EndpointsMethods.GetProvincesByCountryId.Models;
using Location.Contracts.GetProvincesByCountryId;
using Mapster;
using MediatR;

namespace Location.Api.EndpointsMethods.GetProvincesByCountryId;

public class GetProvincesByCountryIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/countries/{countryId}/provinces", async (
            Guid countryId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetProvincesByCountryIdQuery(countryId);

            var result = await sender.Send(query, cancellationToken);

            var response = result.Adapt<GetProvincesByCountryIdResponse>();
            return Results.Ok(response);
        })
            .WithName("GetProvincesByCountryId")
            .WithSummary("GetProvincesByCountryId")
            .WithDescription("Get all Provinces by Country ID")
            .Produces<GetProvincesByCountryIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
