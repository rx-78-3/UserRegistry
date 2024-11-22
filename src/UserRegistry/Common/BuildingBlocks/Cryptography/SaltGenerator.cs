using System.Security.Cryptography;
using BuildingBlocks.Cryptography.Abstractions;

namespace BuildingBlocks.Cryptography;

public class SaltGenerator : ISaltGenerator
{
    public string GenerateString(int size = 16)
    {
        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[size];
        rng.GetBytes(salt);

        return Convert.ToBase64String(salt);
    }
}
