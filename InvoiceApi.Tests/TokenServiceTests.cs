
using InvoiceApi.Services;
using InvoiceApi.Settings;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Xunit;

namespace InvoiceApi.Tests
{
    [Trait("Category", "Unit")]
    public class TokenServiceTests
    {
        [Fact]
        public void GenerateToken_WithValidInputs_ReturnsToken()
        {
            // Arrange
            var username = "test-user";
            var userId = 123;

            var jwtSettings = new JwtSettings
            {
                SecretKey = "InvoiceApi_Test_SecretKey_2026_123456789",
                Issuer = "InvoiceApi",
                Audience = "InvoiceApiUsers",
                ExpiryMinutes = 120
            };

            var options = Options.Create(jwtSettings);
            var tokenService = new TokenService(options);

            // Act
            var token = tokenService.GenerateToken(username, userId);

            // Assert
            Assert.NotEmpty(token);
        }

        [Fact]
        public void GenerateToken_WithValidInputs_ReturnsReadableJwt()
        {
            // Arrange
            var username = "test-user";
            var userId = 123;

            var jwtSettings = new JwtSettings
            {
                SecretKey = "InvoiceApi_Test_SecretKey_2026_123456789",
                Issuer = "InvoiceApi",
                Audience = "InvoiceApiUsers",
                ExpiryMinutes = 120
            };

            var options = Options.Create(jwtSettings);
            var tokenService = new TokenService(options);

            // Act
            var token = tokenService.GenerateToken(username, userId);
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            // Assert
            Assert.NotNull(jwt);
        }

        [Fact]
        public void GenerateToken_WithValidInputs_ContainsExpectedClaims()
        {
            // Arrange
            var username = "test-user";
            var userId = 123;

            var jwtSettings = new JwtSettings
            {
                SecretKey = "InvoiceApi_Test_SecretKey_2026_123456789",
                Issuer = "InvoiceApi",
                Audience = "InvoiceApiUsers",
                ExpiryMinutes = 120
            };

            var options = Options.Create(jwtSettings);
            var tokenService = new TokenService(options);

            // Act
            var token = tokenService.GenerateToken(username, userId);
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var usernameClaim = jwt.Claims.FirstOrDefault(c => c.Type == "Username");
            var userIdClaim = jwt.Claims.FirstOrDefault(c => c.Type == "UserId");

            // Assert
            Assert.NotNull(usernameClaim);
            Assert.Equal(username, usernameClaim.Value);

            Assert.NotNull(userIdClaim);
            Assert.Equal(userId.ToString(), userIdClaim.Value);
        }

        [Fact]
        public void GenerateToken_WithValidInputs_ContainsExpectedIssuerAndAudience()
        {
            // Arrange
            var username = "test-user";
            var userId = 123;

            var jwtSettings = new JwtSettings
            {
                SecretKey = "InvoiceApi_Test_SecretKey_2026_123456789",
                Issuer = "InvoiceApi",
                Audience = "InvoiceApiUsers",
                ExpiryMinutes = 120
            };

            var options = Options.Create(jwtSettings);
            var tokenService = new TokenService(options);

            // Act
            var token = tokenService.GenerateToken(username, userId);
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            // Assert
            Assert.Equal(jwtSettings.Issuer, jwt.Issuer);
            Assert.Contains(jwtSettings.Audience, jwt.Audiences);
        }

        [Fact]
        public void GenerateToken_WithValidInputs_HasExpectedExpiration()
        {
            // Arrange
            var username = "test-user";
            var userId = 123;

            var jwtSettings = new JwtSettings
            {
                SecretKey = "InvoiceApi_Test_SecretKey_2026_123456789",
                Issuer = "InvoiceApi",
                Audience = "InvoiceApiUsers",
                ExpiryMinutes = 120
            };

            var options = Options.Create(jwtSettings);
            var tokenService = new TokenService(options);

            // Act
            var beforeGeneration = DateTime.UtcNow;
            var token = tokenService.GenerateToken(username, userId);
            var afterGeneration = DateTime.UtcNow;

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var expectedExpirationFrom = beforeGeneration
                .AddMinutes(jwtSettings.ExpiryMinutes)
                .AddSeconds(-1);

            var expectedExpirationTo = afterGeneration
                .AddMinutes(jwtSettings.ExpiryMinutes)
                .AddSeconds(1);

            // Assert
            Assert.InRange(
                jwt.ValidTo,
                expectedExpirationFrom,
                expectedExpirationTo
            );
        }
    }
}
