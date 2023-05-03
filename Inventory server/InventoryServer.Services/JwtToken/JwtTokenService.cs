using InventoryServer.Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace InventoryServer.Services.JwtToken
{
    public class JwtTokenService : IJwtTokenService
    {
        private const string BearerPrefix = "Bearer ";

        private const string IdClaimKey = "Id";
        private const string UserRoleClaimKey = "UserRole";

        private readonly string _issuer;
        private readonly SecurityKey _securityKey;
        private readonly SigningCredentials _signingCredentials;

        public JwtTokenService(string issuer, string secretKey)
        {
            _issuer = issuer;
            _securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
        }

        public string CreateToken(User user)
        {
            var claims = new[]
            {
                new Claim(IdClaimKey, user.Id.ToString()),
                new Claim(UserRoleClaimKey, user.UserRole.ToString())
            };

            var jwtSecurityToken = new JwtSecurityToken(_issuer, null, claims, null, null, _signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public CheckTokenResult CheckToken(string token)
        {
            if (token?.StartsWith(BearerPrefix, StringComparison.OrdinalIgnoreCase) == true)
                token = token.Substring(BearerPrefix.Length);

            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = false,
                ValidateLifetime = false,
                IssuerSigningKey = _securityKey
            };

            try
            {
                var claimsPrincipal =
                    new JwtSecurityTokenHandler().ValidateToken(token, parameters, out var validatedToken);

                var userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == IdClaimKey)?.Value;
                var userRoleString = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == UserRoleClaimKey)?.Value;

                var result = Enum.TryParse<UserRole>(userRoleString, out var userRole)
                    ? userRole
                    : throw new InvalidOperationException("Cannot parse user role");

                return new CheckTokenResult { UserId = userId, UserRole = result };
            }
            catch
            {
                return new CheckTokenResult { IsFaulted = true };
            }
        }
    }
}
