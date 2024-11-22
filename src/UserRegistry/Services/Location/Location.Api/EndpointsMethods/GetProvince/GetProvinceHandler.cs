using BuildingBlocks.Cqrs;
using Location.Api.DataAccess;
using Location.Api.EndpointsMethods.GetProvince.Models;
using Location.Contracts.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Location.Api.EndpointsMethods.GetProvince;

public class GetProvinceHandler(LocationsDbContext locationsDbContext) :
    IQueryHandler<GetProvinceQuery, GetProvinceResult>
{
    public async Task<GetProvinceResult> Handle(GetProvinceQuery query, CancellationToken cancellationToken)
    {
        var province = await locationsDbContext.Provinces
            .SingleOrDefaultAsync(p => p.Id == query.ProvinceId, cancellationToken);

        var provinceDto = province?.Adapt<ProvinceDto>();
        return new GetProvinceResult(provinceDto);
    }
}
