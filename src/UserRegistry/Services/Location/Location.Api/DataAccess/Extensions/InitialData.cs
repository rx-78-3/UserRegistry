using Location.Api.DataAccess.Models;

namespace Location.Api.DataAccess.Extensions;

internal static class InitialData
{
    internal static Country[] Countries =
    {
        new Country { Id = Guid.Parse("13201D0D-75EB-46E9-8C28-7E22FC7847A4"), Name = "Canada" },
        new Country { Id = Guid.Parse("41749621-2864-4467-9EA3-7F8DBDB7F14B"), Name = "United States" }
    };

    internal static Province[] Provinces =
    {
        new Province { Id = Guid.Parse("697D12C2-A8B5-44EF-A37B-16576CE03BB6"), CountryId = Guid.Parse("13201D0D-75EB-46E9-8C28-7E22FC7847A4"), Name = "Alberta" },
        new Province { Id = Guid.Parse("A07BF7EE-85F6-4023-8F8C-F1F37B3F9129"), CountryId = Guid.Parse("13201D0D-75EB-46E9-8C28-7E22FC7847A4"), Name = "Ontario" },
        new Province { Id = Guid.Parse("4ADF0BE4-E9CB-498E-87ED-76B4E01A77E1"), CountryId = Guid.Parse("13201D0D-75EB-46E9-8C28-7E22FC7847A4"), Name = "Manitoba" },
        new Province { Id = Guid.Parse("6B5F685D-A61A-4930-B136-DEC64120A6DE"), CountryId = Guid.Parse("13201D0D-75EB-46E9-8C28-7E22FC7847A4"), Name = "Quebec" },

        new Province { Id = Guid.Parse("6FFC4814-06D7-4817-B7F4-3066438B40F9"), CountryId = Guid.Parse("41749621-2864-4467-9EA3-7F8DBDB7F14B"), Name = "California" },
        new Province { Id = Guid.Parse("86CECAA0-5FEE-427A-97F0-FD1F5F892D69"), CountryId = Guid.Parse("41749621-2864-4467-9EA3-7F8DBDB7F14B"), Name = "Texas" },
        new Province { Id = Guid.Parse("56EB735F-0828-407F-ADB8-E75A6D49FAB2"), CountryId = Guid.Parse("41749621-2864-4467-9EA3-7F8DBDB7F14B"), Name = "New York" },
        new Province { Id = Guid.Parse("C2142E90-DF61-457F-898D-132E4125C8CF"), CountryId = Guid.Parse("41749621-2864-4467-9EA3-7F8DBDB7F14B"), Name = "Florida" },
    };
}
