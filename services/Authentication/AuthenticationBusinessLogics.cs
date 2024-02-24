using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api_AIKO.Interfaces;
using Api_AIKO.Models.JwtConfiguration;

namespace Api_AIKO.services.Authentication
{
    public class AuthenticationBusinessLogics : IAuthenticationBusinessLogics
    {
        public AuthenticationBusinessLogics()
        {
        }

        public string GenerateToken(string username, JwtConfiguration jwtConfiguration)
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(jwtConfiguration.Issuer,
                jwtConfiguration.Audience,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
