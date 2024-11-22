namespace BuildingBlocks.Cryptography.Abstractions;

public interface ISaltGenerator
{
    string GenerateString(int size = 16);
}
