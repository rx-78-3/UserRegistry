using Domain.Base;

namespace UserManagement.Domain.ValueObjects;

public record UserLocation
{ 
    public Country Country { get; }
    public Province Province { get; }

    private UserLocation(Country country, Province province) => (Country, Province) = (country, province);

    public static UserLocation Of(Guid countryId, Guid provinceId)
    {
        return new UserLocation(Country.Of(countryId), Province.Of(provinceId));
    }

    public static UserLocation Of(Country country, Province province)
    {
        return new UserLocation(country, province);
    }
}
