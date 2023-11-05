using BetterMomshWebAPI.Models.JWT_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BetterMomshWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("Patient")]
        [Authorize(Roles = "Patient")]
        public IActionResult GetUser()
        {
            var currentUser = GetCurrentUser();

            return Ok(currentUser);
        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                DateTime? birthdate = ParseBirthdate(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.DateOfBirth)?.Value);
                decimal? contactNum = ParseContactNumber(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value);
                Guid? user_id = ParseId(userClaims.FirstOrDefault(o => o.Type == "Id")?.Value);
                return new UserModel
                {
                    user_id = user_id,
                    username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    FirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    MiddleName = userClaims.FirstOrDefault(o => o.Type == "MiddleName")?.Value,
                    LastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                    Birthdate = birthdate,
                    ContactNumber = contactNum,
                    Address = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.StreetAddress)?.Value,
                    Occupation = userClaims.FirstOrDefault(o => o.Type == "Occupation")?.Value,
                    Religion = userClaims.FirstOrDefault(o => o.Type == "Religion")?.Value,
                    RelationshipStatus = userClaims.FirstOrDefault(o => o.Type == "RelationshipStatus")?.Value,

                };
            }
            return null;
        }

        private DateTime? ParseBirthdate(string birthdateClaim)
        {
            if (!string.IsNullOrEmpty(birthdateClaim) && DateTime.TryParse(birthdateClaim, out DateTime birthdate))
            {
                return birthdate;
            }
            return null; // Return null if the birthdate claim is missing or invalid
        }
        private decimal? ParseContactNumber(string contactClaim)
        {
            if (!string.IsNullOrEmpty(contactClaim) && decimal.TryParse(contactClaim, out decimal contactNum))
            {
                return contactNum;
            }
            return null; // Return null if the birthdate claim is missing or invalid
        }

        private Guid? ParseId(string user_id)
        {
            if (!string.IsNullOrEmpty(user_id) && Guid.TryParse(user_id, out Guid userId))
            {
                // If the string is successfully parsed to a Guid, assign it to the user_id
                return userId;
            }
            return null;
        }
    }
}
