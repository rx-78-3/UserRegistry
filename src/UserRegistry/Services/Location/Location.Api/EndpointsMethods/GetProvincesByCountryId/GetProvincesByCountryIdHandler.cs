using BuildingBlocks.Cqrs;
using Location.Api.DataAccess;
using Location.Api.EndpointsMethods.GetProvincesByCountryId.Models;
using Location.Contracts.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Location.Api.EndpointsMethods.GetProvincesByCountryId;

public class GetProvincesByCountryIdHandler(LocationsDbContext locationsDbContext) :
    IQueryHandler<GetProvincesByCountryIdQuery, GetProvincesByCountryIdResult>
{
    public async Task<GetProvincesByCountryIdResult> Handle(GetProvincesByCountryIdQuery query, CancellationToken cancellationToken)
    {
        var provinces = await locationsDbContext.Provinces
            .Where(p => p.CountryId == query.CountryId)
            .ToArrayAsync(cancellationToken);

        var provincesDto = provinces.Adapt<ProvinceSummaryDto[]>();
        return new GetProvincesByCountryIdResult(provincesDto);
    }
}
