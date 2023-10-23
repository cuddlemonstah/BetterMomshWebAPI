using BetterMomshWebAPI.EFCore;
using BetterMomshWebAPI.Models;
using BetterMomshWebAPI.Models.Responses;
using BetterMomshWebAPI.Utils.Services;
using BetterMomshWebAPI.Utils.TokenValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterMomshWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DbHelper _db;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        public AuthController(API_DataContext _DataContext, AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator,
            RefreshTokenValidator refreshTokenValidator)
        {
            _db = new DbHelper(_DataContext);
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenValidator = refreshTokenValidator;
        }

        // POST api/<AuthController>/UserRegister
        [AllowAnonymous]
        [HttpPost("UserRegister")]
        public IActionResult PostReg([FromBody] RegistrationModel value)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var result = _db.RegisterUser(value);
                if (result == "Registered Successfully")
                {
                    return Ok(ResponseHandler.GetAppResponse(type, result));
                }
                else
                {
                    return Unauthorized(ResponseHandler.GetAppResponse(type, result));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        /// <summary>
        /// Log in 
        /// </summary>
        /// <param name="value">value of the inputs</param>
        /// <returns>Returns Http Request</returns>

        [AllowAnonymous]
        [HttpPost("UserLogin")]
        public async Task<IActionResult> Login([FromBody] LoginModel value)
        {
            try
            {
                var user = _db.Authenticate(value);
                if (user != null)
                {

                    var accesstoken = _accessTokenGenerator.Generate(user);
                    var refreshtoken = _refreshTokenGenerator.GenerateToken();

                    return Ok(new AuthenticatedUserResponses()
                    {
                        AccessToken = accesstoken,
                        RefreshToken = refreshtoken
                    });
                }
                else
                {
                    return Unauthorized("Invalid credentials");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }

                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }


        /* [HttpPost("refresh")]
         public async Task<IActionResult> Refresh([FromBody] RefreshRequest refreshRequest)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequestModelState();
             }
             bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
             if (!isValidRefreshToken)
             {
                 return BadRequest(new ErrorResponse("Invalid refresh token"));
             }

         }*/



        private IActionResult BadRequestModelState()
        {
            IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new ErrorResponse(errorMessages));
        }
    }
}
