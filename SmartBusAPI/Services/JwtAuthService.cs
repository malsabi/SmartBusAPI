using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SmartBusAPI.Common.Interfaces.Services;
using SmartBusAPI.Persistence.Authentication;

namespace SmartBusAPI.Services
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly JwtSettings jwtSettings;

        public JwtAuthService(IOptions<JwtSettings> jwtSettings)
        {
            this.jwtSettings = jwtSettings.Value;
        }

        public string GenerateAuthToken(string firstName, string lastName, string role)
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, $"{firstName} {lastName}"),
                new Claim(ClaimTypes.Role, role)
            };

            return GenerateToken(claims);
        }

        private string GenerateToken(Claim[] claims)
        {
            SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)), SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.Now.AddDays(jwtSettings.ExpiryDays),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}