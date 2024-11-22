using BuildingBlocks.Cqrs;
using Location.Api.DataAccess;
using Location.Api.EndpointsMethods.GetProvinces.Models;
using Location.Contracts.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Location.Api.EndpointsMethods.GetProvinces;

public class GetProvincesHandler(LocationsDbContext locationsDbContext) :
    IQueryHandler<GetProvincesQuery, GetProvincesResult>
{
    public async Task<GetProvincesResult> Handle(GetProvincesQuery query, CancellationToken cancellationToken)
    {
        var provinces = await locationsDbContext.Provinces
            .Include(p => p.Country)
            .ToArrayAsync();

        var provincesDto = provinces.Adapt<ProvinceCountryDto[]>();
        return new GetProvincesResult(provincesDto);
    }
}
