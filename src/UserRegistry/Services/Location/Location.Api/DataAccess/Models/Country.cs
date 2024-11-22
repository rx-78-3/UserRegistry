namespace Location.Api.DataAccess.Models;

public class Country
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Province>? Provinces { get; set; }
}
