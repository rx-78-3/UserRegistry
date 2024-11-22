using System.Security.Cryptography;
using System.Text;
using BuildingBlocks.Cryptography.Abstractions;

namespace BuildingBlocks.Cryptography;

public class PasswordHasher : IPasswordHasher
{
    public string ComputeHash(string input, string saltString)
    {
        using (var sha256 = SHA256.Create())
        {
            var saltedInput = $"{input}{saltString}";
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedInput));
            var builder = new StringBuilder();

            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            var hashedPassword = $"{builder.ToString()}:{saltString}";
            return hashedPassword;
        }
    }
}
