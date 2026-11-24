using InvoiceApi.Security;
using Xunit;

namespace InvoiceApi.Tests
{
    public class BCryptPasswordHasherTests
    {
        readonly IPasswordHasher _passwordHasher;

        public BCryptPasswordHasherTests()
        {
            _passwordHasher = new BCryptPasswordHasher();
        }
        [Fact]
        public void VerifyPassword_WithCorrectPassword_ReturnsTrue()
        {
            var pass = "Mahla";
            var hassPass= _passwordHasher.HashPassword(pass);
            var result = _passwordHasher.VerifyPassword(pass, hassPass);
            Assert.True(result);
        }


        [Fact]
        public void VerifyPassword_WithIncorrectPassword_ReturnsFalse()
        {
            var pass = "Mahla";
            var fakePass = "Mobin";
            var hassPass = _passwordHasher.HashPassword(fakePass);
            var result = _passwordHasher.VerifyPassword(pass, hassPass);
            Assert.False(result);
        }

        [Fact]
        public void HashPassword_WithSamePassword_ReturnsDifferentHashes()
        {
            var pass = "Mahla";
            var expected = _passwordHasher.HashPassword(pass);
            var actual = _passwordHasher.HashPassword(pass);
    
            Assert.NotEqual(expected, actual);
        }
    }
}
