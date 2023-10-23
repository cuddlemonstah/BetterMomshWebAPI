using BetterMomshWebAPI.Models.Configuration;
using BetterMomshWebAPI.Models.JWT_Models;
using System.Security.Claims;

namespace BetterMomshWebAPI.Utils.Services
{
    public class AccessTokenGenerator
    {
        private readonly AuthenticationConfiguration _configuration;
        private readonly TokenGenerator _tokenGenerator;

        public AccessTokenGenerator(AuthenticationConfiguration configuration, TokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }
        /// <summary>
        /// It Generates a JWToken for Authorizing Users
        /// </summary>
        /// 
        /// <param name="user">Its the User Info in the Registration Model/User Model</param>
        /// <returns>JWT Token</returns>

        public string Generate(UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.username),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim("MiddleName", user.MiddleName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.DateOfBirth, user.Birthdate.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.ContactNumber.ToString()),
                new Claim(ClaimTypes.StreetAddress, user.Address),
                new Claim("Occupation", user.Occupation),
                new Claim("Religion", user.Religion),
                new Claim("Relationship", user.RelationshipStatus)
            };
            return _tokenGenerator.GenerateToken(_configuration.Key,
                _configuration.Issuer,
                _configuration.Audience,
                _configuration.AccessTokenExpirationMinutes,
                claims);
        }
    }
}
