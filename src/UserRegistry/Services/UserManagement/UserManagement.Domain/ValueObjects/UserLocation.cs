using Domain.Base;

namespace UserManagement.Domain.ValueObjects;

public record UserLocation
{ 
    public Guid CountryId { get; }

    public Guid ProvinceId { get; }

    private UserLocation(Guid countryId, Guid provinceId) => (CountryId, ProvinceId) = (countryId, provinceId);

    public static UserLocation Of(Guid countryId, Guid provinceId)
    {
        if (countryId == Guid.Empty)
        {
            throw new DomainException("CountryId cannot be empty.");
        }
        if (provinceId == Guid.Empty)
        {
            throw new DomainException("ProvinceId cannot be empty.");
        }

        return new UserLocation(countryId, provinceId);
    }
}
