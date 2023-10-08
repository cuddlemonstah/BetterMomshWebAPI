using BetterMomshWebAPI.EFCore;
using BetterMomshWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BetterMomshWebAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DbHelper _db;
        public AuthController(API_DataContext _DataContext)
        {
            _db = new DbHelper(_DataContext);
        }
        // GET: api/<AuthController>

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        [Route("[controller]/UserInfoById/{id}")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                RegistrationModel data = _db.GetUserById(id);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {

                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<AuthController>
        [HttpPost]
        [Route("[controller]/UserRegister")]
        public IActionResult PostReg([FromBody] RegistrationModel value)
        {

            try
            {
                ResponseType type = ResponseType.Success;
                var result = _db.RegisterUser(value);
                return Unauthorized(ResponseHandler.GetAppResponse(type, result));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("[controller]/UserLogin")]
        public IActionResult Post([FromBody] LoginModel value)
        {

            try
            {
                ResponseType type = ResponseType.Success;
                var result = _db.LoginUser(value);
                return Unauthorized(ResponseHandler.GetAppResponse(type, result));
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
