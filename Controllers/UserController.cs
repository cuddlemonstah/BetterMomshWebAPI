using BetterMomshWebAPI.Models;
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

        private RegistrationModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                DateTime? birthdate = ParseBirthdate(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.DateOfBirth)?.Value);
                decimal? contactNum = ParseContactNumber(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value);
                return new RegistrationModel
                {
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

    }
}
