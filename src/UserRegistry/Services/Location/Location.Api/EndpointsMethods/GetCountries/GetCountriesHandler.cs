using BuildingBlocks.Cqrs;
using Location.Api.DataAccess;
using Location.Api.EndpointsMethods.GetCountries.Models;
using Location.Contracts.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Location.Api.EndpointsMethods.GetCountries;

internal class GetCountriesHandler(LocationsDbContext locationsDbContext) :
    IQueryHandler<GetCountriesQuery, GetCountriesResult>
{
    public async Task<GetCountriesResult> Handle(GetCountriesQuery query, CancellationToken cancellationToken)
    {
        var countries = await locationsDbContext.Countries.ToArrayAsync(cancellationToken);

        var countriesDto = countries.Adapt<CountryDto[]>();
        return new GetCountriesResult(countriesDto);
    }
}
