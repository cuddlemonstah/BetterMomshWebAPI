using BetterMomshWebAPI.Models.JWT_Models;
using BetterMomshWebAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BetterMomshWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Common _common;

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;

                DateTime? birthdate = _common.ParseBirthdate(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.DateOfBirth)?.Value);
                decimal? contactNum = _common.ParseContactNumber(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value);
                Guid? user_id = _common.ParseId(userClaims.FirstOrDefault(o => o.Type == "Id")?.Value);
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


    }
}
