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
            var claims = new List<Claim>();

            if (user != null)
            {
                if (user.user_id != null)
                {
                    claims.Add(new Claim("Id", user.user_id.ToString()));
                }
                if (user.username != null)
                {
                    claims.Add(new Claim(ClaimTypes.Email, user.username));
                }
                if (user.FirstName != null)
                {
                    claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
                }
                if (user.MiddleName != null)
                {
                    claims.Add(new Claim("MiddleName", user.MiddleName));
                }
                if (user.LastName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
                }
                if (user.Role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));
                }
                if (user.Birthdate != null)
                {
                    claims.Add(new Claim(ClaimTypes.DateOfBirth, user.Birthdate.ToString()));
                }
                if (user.ContactNumber != null)
                {
                    claims.Add(new Claim(ClaimTypes.MobilePhone, user.ContactNumber.ToString()));
                }
                if (user.Address != null)
                {
                    claims.Add(new Claim(ClaimTypes.StreetAddress, user.Address));
                }
                if (user.Occupation != null)
                {
                    claims.Add(new Claim("Occupation", user.Occupation));
                }
                if (user.Religion != null)
                {
                    claims.Add(new Claim("Religion", user.Religion));
                }
                if (user.RelationshipStatus != null)
                {
                    claims.Add(new Claim("Relationship", user.RelationshipStatus));
                }
            }

            return _tokenGenerator.GenerateToken(_configuration.Key,
                _configuration.Issuer,
                _configuration.Audience,
                _configuration.AccessTokenExpirationMinutes,
                claims);
        }
    }
}
