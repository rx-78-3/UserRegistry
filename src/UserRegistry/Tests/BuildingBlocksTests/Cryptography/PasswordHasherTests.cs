using BuildingBlocks.Cryptography;

namespace CoreServicesTests.Cryptography;

[TestClass]
public class PasswordHasherTests
{
    private PasswordHasher _passwordHasher = default!;

    [TestInitialize]
    public void Setup()
    {
        _passwordHasher = new PasswordHasher();
    }

    [TestMethod]
    public void ComputeHash_ValidInputAndSalt_ReturnsExpectedHash()
    {
        // Arrange
        var input = "password";
        var salt = "salt";
        var expectedHash = "7a37b85c8918eac19a9089c0fa5a2ab4dce3f90528dcdeec108b23ddf3607b99:salt";

        // Act
        var result = _passwordHasher.ComputeHash(input, salt);

        // Assert
        Assert.AreEqual(expectedHash, result);
    }

    [TestMethod]
    public void ComputeHash_DifferentInputSameSalt_ReturnsDifferentHash()
    {
        // Arrange
        var input1 = "password1";
        var input2 = "password2";
        var salt = "salt";

        // Act
        var result1 = _passwordHasher.ComputeHash(input1, salt);
        var result2 = _passwordHasher.ComputeHash(input2, salt);

        // Assert
        Assert.AreNotEqual(result1, result2);
    }

    [TestMethod]
    public void ComputeHash_SameInputDifferentSalt_ReturnsDifferentHash()
    {
        // Arrange
        var input = "password";
        var salt1 = "salt1";
        var salt2 = "salt2";

        // Act
        var result1 = _passwordHasher.ComputeHash(input, salt1);
        var result2 = _passwordHasher.ComputeHash(input, salt2);

        // Assert
        Assert.AreNotEqual(result1, result2);
    }
}
