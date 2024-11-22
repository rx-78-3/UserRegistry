namespace BuildingBlocks.Cryptography.Abstractions;
public interface IPasswordHasher
{
    string ComputeHash(string input, string saltString);
}
