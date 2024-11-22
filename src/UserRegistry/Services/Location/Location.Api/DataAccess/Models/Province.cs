namespace Location.Api.DataAccess.Models;

public class Province
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid CountryId { get; set; }
    public Country? Country { get; set; }
}
