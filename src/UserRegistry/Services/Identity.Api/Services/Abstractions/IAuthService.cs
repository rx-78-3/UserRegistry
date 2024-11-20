using UserManagement.DataAccess.Models;

namespace Identity.Api.Services.Abstractions;

public interface IAuthService
{
    Task<User?> ValidateUserAsync(string email, string password, CancellationToken cancellationToken);
    string GenerateJwtToken(User user);
}
