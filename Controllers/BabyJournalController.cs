using BetterMomshWebAPI.EFCore;
using BetterMomshWebAPI.Models;
using BetterMomshWebAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BetterMomshWebAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BabyJournalController : ControllerBase
    {

        private readonly DbHelper _db;
        public BabyJournalController(API_DataContext _DataContext)
        {
            _db = new DbHelper(_DataContext);
        }
        [HttpPost("BabyBook")]
        public IActionResult PostBBook([FromBody] BabyBookModel value)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var result = _db.CreateBabyBook(value);
                if (result == "New Baby Journal Added")
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

        [HttpPost("Journal")]
        public IActionResult PostJorn([FromBody] JournalModel value)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var result = _db.AddJournal(value);
                if (result == "New Baby Journal Added")
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

        [HttpGet("BbookBy/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                BabyBookModel data = _db.GetBbookById(id);
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
    }
}
