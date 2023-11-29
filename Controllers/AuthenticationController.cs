using BetterMomshWebAPI.EFCore;
using BetterMomshWebAPI.Models;
using BetterMomshWebAPI.Models.JWT_Models;
using BetterMomshWebAPI.Models.Responses;
using BetterMomshWebAPI.Utils;
using BetterMomshWebAPI.Utils.TokenValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterMomshWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly DbHelper _db;
        private readonly Authenticator _authenticator;
        private readonly RefreshTokenValidator _refreshTokenValidator;

        public AuthenticationController(API_DataContext _DataContext,
            RefreshTokenValidator refreshTokenValidator,
            Authenticator authenticator)
        {
            _db = new DbHelper(_DataContext);
            _authenticator = authenticator;
            _refreshTokenValidator = refreshTokenValidator;
        }

        // POST api/<AuthController>/UserRegister
        [AllowAnonymous]
        [HttpPost("user-register")]
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
        [HttpPost("user-login")]
        public async Task<IActionResult> Login([FromBody] LoginModel value)
        {
            try
            {
                UserModel user = _db.Authenticate(value);
                if (user != null)
                {

                    AuthenticatedUserResponses response = await _authenticator.Authenticate(user);
                    return Ok(response);
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
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutModel model)
        {
            // Ensure that the token is not null/empty
            if (string.IsNullOrEmpty(model.Token))
            {
                return BadRequest("Invalid token");
            }

            // Add the token to the blacklist
            await _db.AddToBlacklistAsync(model.Token);
            return Ok("Token added to the blacklist");
        }

        [HttpPost("refresh-token")]
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

            EFCore.RefreshToken refreshTokenDTO = await _db.GetByToken(refreshRequest.RefreshToken);
            if (refreshTokenDTO == null)
            {
                return NotFound(new ErrorResponse("Invalid refresh token."));

            }
            UserModel user = await _db.getUserID(refreshTokenDTO.user_id);
            if (user == null)
            {
                return NotFound(new ErrorResponse("User Not Found"));
            }
            AuthenticatedUserResponses response = await _authenticator.Authenticate(user);
            return Ok(response);
        }

        private IActionResult BadRequestModelState()
        {
            IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            return BadRequest(new ErrorResponse(errorMessages));
        }
    }
}
