using Domain.Base;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Domain.Models.Tests;

[TestClass]
public class UserTests
{
    [TestMethod]
    public void Create_ShouldReturnUser_WithGivenParameters()
    {
        // Arrange
        var userId = UserId.Of(Guid.NewGuid());
        var email = Email.Of("test@example.com");
        var passwordHash = PasswordHash.Of("hashedPassword");
        var location = UserLocation.Of(Guid.NewGuid(), Guid.NewGuid());

        // Act
        var user = User.Create(userId, email, passwordHash, location);

        // Assert
        Assert.AreEqual(userId, user.Id);
        Assert.AreEqual(email, user.Email);
        Assert.AreEqual(passwordHash, user.PasswordHash);
        Assert.AreEqual(location, user.Location);
    }

    [TestMethod]
    public void Email_Of_ShouldReturnEmail_WithGivenValue()
    {
        // Arrange
        var emailValue = "test@example.com";

        // Act
        var email = Email.Of(emailValue);

        // Assert
        Assert.AreEqual(emailValue, email.Value);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainException))]
    public void Email_Of_ShouldThrowException_WhenEmailIsInvalid()
    {
        // Arrange
        var invalidEmailValue = "invalid-email";

        // Act
        Email.Of(invalidEmailValue);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Email_Of_ShouldThrowException_WhenEmailIsNull()
    {
        // Arrange
        string? invalidEmailValue = null;

        // Act
        Email.Of(invalidEmailValue);
    }

    [TestMethod]
    public void PasswordHash_Of_ShouldReturnPasswordHash_WithGivenValue()
    {
        // Arrange
        var passwordHashValue = "hashedPassword";

        // Act
        var passwordHash = PasswordHash.Of(passwordHashValue);

        // Assert
        Assert.AreEqual(passwordHashValue, passwordHash.Value);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void PasswordHash_O_ShouldThrowException_WhenPasswordHashIsEmpty()
    {
        // Arrange
        string? invalidEmailValue = null;

        // Act
        PasswordHash.Of(invalidEmailValue);
    }

    [TestMethod]
    public void UserLocation_Of_ShouldReturnUserLocation_WithGivenCountryAndProvinceIds()
    {
        // Arrange
        var countryId = Guid.NewGuid();
        var provinceId = Guid.NewGuid();

        // Act
        var location = UserLocation.Of(countryId, provinceId);

        // Assert
        Assert.AreEqual(countryId, location.Country.Id);
        Assert.AreEqual(provinceId, location.Province.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainException))]
    public void UserLocation_Of_ShouldThrowException_WhenCountryIdIsEmpty()
    {
        // Arrange
        var countryId = Guid.Empty;
        var provinceId = Guid.NewGuid();

        // Act
        var location = UserLocation.Of(countryId, provinceId);
    }

    [TestMethod]
    [ExpectedException(typeof(DomainException))]
    public void UserLocation_Of_ShouldThrowException_WhenProvinceIdIsEmpty()
    {
        // Arrange
        var countryId = Guid.NewGuid();
        var provinceId = Guid.Empty;

        // Act
        var location = UserLocation.Of(countryId, provinceId);
    }

    [TestMethod]
    public void UserLocation_Of_ShouldReturnUserLocation_WithGivenCountryAndProvince()
    {
        // Arrange
        var country = Country.Of(Guid.NewGuid(), "CountryName");
        var province = Province.Of(Guid.NewGuid(), "ProvinceName");

        // Act
        var location = UserLocation.Of(country, province);

        // Assert
        Assert.AreEqual(country, location.Country);
        Assert.AreEqual(province, location.Province);
    }
}