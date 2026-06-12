using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System;

namespace InvoiceApi.Helpers
{
    public static class JwtHelper
    {
        private static string secret = "THIS_IS_MY_SECRET_KEY_12345";

        public static string GenerateToken(string username, int userId)
        {
            //generate jwt key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("UserId", userId.ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static ClaimsPrincipal? ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                var key = Encoding.UTF8.GetBytes(secret);

                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                return handler.ValidateToken(token, parameters, out _);
            }
            catch
            {
                return null;
            }
        }
    }
}
