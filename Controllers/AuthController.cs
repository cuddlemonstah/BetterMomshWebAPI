using BetterMomshWebAPI.EFCore;
using BetterMomshWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BetterMomshWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DbHelper _db;

        public AuthController(API_DataContext _DataContext)
        {
            _db = new DbHelper(_DataContext);
        }

        // GET api/<AuthController>/5
        [HttpGet("UserInfoById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                RegistrationModel data = _db.GetUserById(id);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<AuthController>/UserRegister
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
                    // Add WWW-Authenticate header with challenge
                    Response.Headers.Add("WWW-Authenticate", "Bearer realm=\"BetterMomshWebAPI\"");
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

        [HttpPost("UserLogin")]
        public IActionResult PostLog([FromBody] LoginModel value)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var result = _db.LoginUser(value);
                if (result.StartsWith("Logged In"))
                {
                    return Ok(ResponseHandler.GetAppResponse(type, result));
                }
                else
                {
                    // Add WWW-Authenticate header with challenge
                    Response.Headers.Add("WWW-Authenticate", "Bearer realm=\"BetterMomshWebAPI\"");
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
    }
}