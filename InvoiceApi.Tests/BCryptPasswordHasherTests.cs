using InvoiceApi.Security;
using Xunit;

namespace InvoiceApi.Tests
{
    [Trait("Category", "Unit")]
    public class BCryptPasswordHasherTests
    {
        private readonly IPasswordHasher _passwordHasher;

        public BCryptPasswordHasherTests()
        {
            _passwordHasher = new BCryptPasswordHasher();
        }

        [Fact]
        public void VerifyPassword_WithCorrectPassword_ReturnsTrue()
        {
            // Arrange
            var password = "Mahla";
            var hash = _passwordHasher.HashPassword(password);

            // Act
            var result = _passwordHasher.VerifyPassword(password, hash);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_WithIncorrectPassword_ReturnsFalse()
        {
            // Arrange
            var correctPassword = "Mahla";
            var wrongPassword = "Mobin";
            var hash = _passwordHasher.HashPassword(correctPassword);

            // Act
            var result = _passwordHasher.VerifyPassword(wrongPassword, hash);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HashPassword_WithSamePassword_ReturnsDifferentHashes()
        {
            // Arrange
            var password = "Mahla";

            // Act
            var hash1 = _passwordHasher.HashPassword(password);
            var hash2 = _passwordHasher.HashPassword(password);

            // Assert
            // Every hash gets a new random Salt, so the results differ.
            Assert.NotEqual(hash1, hash2);
        }
    }
}