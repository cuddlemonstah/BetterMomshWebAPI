using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BetterMomshWebAPI.Utils.Services
{
    public class TokenGenerator
    {
        public string GenerateToken(string Key, string Issuer, string Audience, double ExpirationMinutes,
            IEnumerable<Claim> claims = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(Issuer,
                Audience,
                claims,
                expires: DateTime.Now.AddMinutes(ExpirationMinutes),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
