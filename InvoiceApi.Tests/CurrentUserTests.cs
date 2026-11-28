
using InvoiceApi.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using Xunit;

namespace InvoiceApi.Tests
{
    [Trait("Category", "Unit")]
    public class CurrentUserTests
    {
        private readonly HttpContextAccessor _httpContext;
        private readonly DefaultHttpContext _defaultHttpContext;

        public CurrentUserTests()
        {
            _httpContext = new HttpContextAccessor();
            _defaultHttpContext = new DefaultHttpContext();

            _httpContext.HttpContext = _defaultHttpContext;
        }

        [Fact]
        public void GetUserId_WithValidClaim_ReturnsUserId()
        {
            // Arrange
            Claim claim = new Claim("UserId", "123");
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { claim });
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            _defaultHttpContext.User = claimsPrincipal;

            CurrentUser currentUser = new CurrentUser(_httpContext);

            // Act
            int result = currentUser.GetUserId();

            // Assert
            Assert.Equal(123, result);
        }

        [Fact]
        public void GetUserId_WithoutUserIdClaim_ThrowsException()
        {
            // Arrange
            Claim claim = new Claim("user", "123");
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { claim });
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            _defaultHttpContext.User = claimsPrincipal;

            CurrentUser currentUser = new CurrentUser(_httpContext);

            // Act & Assert
            Assert.Throws<Exception>(() => currentUser.GetUserId());
        }

        [Fact]
        public void GetUserId_WithInvalidClaimValue_ThrowsException()
        {
            // Arrange
            Claim claim = new Claim("UserId", "abc");
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { claim });
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            _defaultHttpContext.User = claimsPrincipal;

            CurrentUser currentUser = new CurrentUser(_httpContext);

            // Act & Assert
            Assert.Throws<Exception>(() => currentUser.GetUserId());
        }

        [Fact]
        public void GetUserId_WithZeroUserId_ThrowsException()
        {
            // Arrange
            Claim claim = new Claim("UserId", "0");
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { claim });
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            _defaultHttpContext.User = claimsPrincipal;

            CurrentUser currentUser = new CurrentUser(_httpContext);

            // Act & Assert
            Assert.Throws<Exception>(() => currentUser.GetUserId());
        }

        [Fact]
        public void GetUserId_WithNegativeUserId_ThrowsException()
        {
            // Arrange
            Claim claim = new Claim("UserId", "-1");
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { claim });
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            _defaultHttpContext.User = claimsPrincipal;

            CurrentUser currentUser = new CurrentUser(_httpContext);

            // Act & Assert
            Assert.Throws<Exception>(() => currentUser.GetUserId());
        }
        [Fact]
        public void GetUserId_WithNullHttpContext_ThrowsException()
        {
            // Arrange
            _httpContext.HttpContext = null;

            CurrentUser currentUser = new CurrentUser(_httpContext);

            // Act & Assert
            Assert.Throws<Exception>(() => currentUser.GetUserId());
        }
    }
}
