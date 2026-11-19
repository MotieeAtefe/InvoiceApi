using System;
using InvoiceApi.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvoiceApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtSettings> _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        public string GenerateToken(string username, int userId)
        {
            Claim c1 = new Claim("Username", username);
            Claim c2 = new Claim("UserId", userId.ToString());
            List<Claim> claims = new List<Claim>();
            claims.Add(c1);
            claims.Add(c2);
            var BytesOutput = Encoding.UTF8.GetBytes(_jwtSettings.Value.SecretKey);
            var secret = new SymmetricSecurityKey(BytesOutput);
            var SigningCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _jwtSettings.Value.ExpiryMinutes),
                signingCredentials: SigningCredentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);



        }

    }
}
