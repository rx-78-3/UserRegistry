using Carter;
using Location.Api.EndpointsMethods.GetProvince.Models;
using Location.Contracts.GetProvince;
using Mapster;
using MediatR;

namespace Location.Api.EndpointsMethods.GetProvince;

public class GetProvinceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/provinces/{provinceId}", async (
            Guid provinceId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetProvinceQuery(provinceId);

            var result = await sender.Send(query, cancellationToken);

            if (result.Province is null)
            {
                return Results.NotFound();
            }

            var response = result.Adapt<GetProvinceResponse>();
            return Results.Ok(response);
        })
            .WithName("GetProvince")
            .WithSummary("GetProvince")
            .WithDescription("Get Province by ID")
            .Produces<GetProvinceResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound);
    }
}
