using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BuildingBlocks.Cryptography;
using BuildingBlocks.Cryptography.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using UserManagement.DataAccess;
using UserManagement.DataAccess.Models;

namespace Identity.Api.Services.Tests;

[TestClass()]
public class AuthServiceTests
{
    private const string SecretKey = "your_very_long_secret_key_1234567890";
    private const string CorrectHash = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b:5d41402abc4b2a76b9719d911017c592";

    //private Mock<UsersDbContext> _usersDbContextMock;
    private  Mock<IPasswordHasher> _passwordHasherMock = default!;
    private IPasswordValidator _passwordValidator = default!;
    private IConfiguration _configuration = default!;
    private AuthService _authService = default!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<UsersDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var _usersDbContext = new UsersDbContext(options);

        if (!_usersDbContext.Users.Any())
        {
            _usersDbContext.Add(new User { Email = "testuser@m.com", PasswordHash = CorrectHash });
            _usersDbContext.SaveChanges();
        }

        _passwordHasherMock = new Mock<IPasswordHasher>();
        _passwordValidator = new PasswordValidator(_passwordHasherMock.Object);

        var inMemorySettings = new Dictionary<string, string?> {
            {"JwtSettings:SecretKey", SecretKey},
            {"JwtSettings:Issuer", "your_issuer"},
            {"JwtSettings:Audience", "your_audience"}
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _authService = new AuthService(_configuration, _usersDbContext, _passwordValidator);
    }

    [TestMethod]
    public void GenerateJwtToken_ValidUser_ReturnsToken()
    {
        // Arrange
        var user = new User
        {
            Email = "testuser@m.com",
            PasswordHash = string.Empty
        };

        // Act
        var token = _authService.GenerateJwtToken(user);

        // Assert
        Assert.IsNotNull(token);

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your_issuer",
            ValidAudience = "your_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey))
        };

        SecurityToken validatedToken;
        var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

        Assert.IsNotNull(principal);
        Assert.AreEqual("testuser@m.com", principal.Claims.Single(c => c.Type == "Email").Value);
        Assert.IsTrue(principal.HasClaim(c => c.Type == JwtRegisteredClaimNames.Jti));
    }

    [TestMethod]
    public async Task ValidateUserAsync_UserNotFound_ReturnsNull()
    {
        // Arrange
        // No additional setup needed.

        // Act
        var result = await _authService.ValidateUserAsync("nonexistentuser@m.com", "password", default);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task ValidateUserAsync_PasswordHashDoesNotMatch_ReturnsNull()
    {
        // Arrange
        // No additional setup needed.

        _passwordHasherMock.Setup(hasher => hasher.ComputeHash(It.IsAny<string>(), It.IsAny<string>()))
            .Returns("wronghash");

        // Act
        var result = await _authService.ValidateUserAsync("testuser@m.com", "password", default);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task ValidateUserAsync_PasswordHashMatches_ReturnsUser()
    {
        // Arrange
        // No additional setup needed.

        _passwordHasherMock.Setup(hasher => hasher.ComputeHash(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(CorrectHash);

        // Act
        var result = await _authService.ValidateUserAsync("testuser@m.com", "password", default);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(result.Email, "testuser@m.com");
        Assert.AreEqual(result.PasswordHash, CorrectHash);
    }
}