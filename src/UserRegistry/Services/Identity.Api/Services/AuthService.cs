using System.Security.Claims;
using System.Text;
using CoreServices.Cryptography.Abstractions;
using Identity.Api.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using UserManagement.DataAccess;
using UserManagement.DataAccess.Models;

namespace Identity.Api.Services;

public class AuthService : IAuthService
{
    private readonly string _issuer;
    private readonly string _audience;
    // TODO Change to asymmetric key.
    private readonly SymmetricSecurityKey _key;
    private readonly UsersDbContext _usersDbContext;
    private readonly IPasswordValidator _passwordValidator;

    public AuthService(
        IConfiguration configuration,
        UsersDbContext usersDbContext,
        IPasswordValidator passwordValidator)
    {
        var jwtConfigurationSection = configuration.GetSection("JwtSettings");
        var secretKey = jwtConfigurationSection["SecretKey"]!;

        _issuer = jwtConfigurationSection["Issuer"]!;
        _audience = jwtConfigurationSection["Audience"]!;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        _usersDbContext = usersDbContext;
        _passwordValidator = passwordValidator;
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim("Email", user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<User?> ValidateUserAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _usersDbContext.Users
            .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);

        if (user == null)
        {
            return null;
        }

        if (!_passwordValidator.ValidatePassword(password, user.PasswordHash))
        {
            return null;
        }

        return user;
    }
}
