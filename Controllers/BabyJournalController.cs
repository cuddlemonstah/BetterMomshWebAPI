using BetterMomshWebAPI.EFCore;
using BetterMomshWebAPI.Models;
using BetterMomshWebAPI.Models.Database_Repository;
using BetterMomshWebAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BetterMomshWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("dashboard")]
    public class BabyJournalController : ControllerBase
    {

        private readonly DbHelper _db;
        private readonly Journal_DbHelper _journDb;
        public BabyJournalController(API_DataContext _DataContext)
        {
            _db = new DbHelper(_DataContext);
            _journDb = new Journal_DbHelper(_DataContext);
        }
        [HttpPost("user={userId}/add/baby-book")]
        public IActionResult AddBabyBook(Guid userId, [FromBody] BabyBookModel value)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                var result = _db.CreateBabyBook(userId, value);
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
                Console.WriteLine("Inner Exce  ption: " + ex.InnerException?.Message);
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost("user={userId}/add/baby-journal")]
        public IActionResult AddBabyJournal(Guid userId, [FromBody] JournalModel value)
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

        [HttpGet("book/user={userId}&book-id={id}")]
        public IActionResult GetBookById(int id)
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
        [HttpPut("book/update/user={userId}&book-id{id}")]
        public IActionResult UpdateJournal([FromBody] JournalModel value, long id)
        {
            try
            {
                ResponseType e = ResponseType.Success;
                var data = _journDb.UpdateJournal(value, id);
                if (data == null)
                {
                    e = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(e, data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("Inner Exception: " + e.InnerException?.Message);
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }
        [HttpDelete("book/delete/user={userId}&book-id{id}")]
        public async Task<IActionResult> DeleteJournal(long id)
        {
            try
            {
                ResponseType e = ResponseType.Success;
                var data = await _journDb.DeleteJournal(id);
                if (!data)
                {
                    e = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(e, data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("Inner Exception: " + e.InnerException?.Message);
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }

        [HttpGet("book/user={userId}")]
        public async Task<IActionResult> GetAllBabyBook(Guid userID)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                List<BabyBook> data = await _journDb.GetAllBabyBooks(userID);
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
        [HttpGet("book/journal/user={UserID}&book={BookID}/trimester")]
        public async Task<IActionResult> GetAllBabyBookTrim(Guid userID, long bookID)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                List<Trimester> data = await _journDb.GetAllTrimesters(userID, bookID);
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
        [HttpGet("book/journal/user={UserID}&book={BookID}/{trimId}/month")]
        public async Task<IActionResult> GetAllBabyBookMonth(Guid userID, long bookID, long trimID)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                List<Month> data = await _journDb.GetAllMonths(userID, bookID, trimID);
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
        [HttpGet("book/journal/user={UserID}&book={BookID}/{monthId}/week")]
        public async Task<IActionResult> GetAllBbookWeek(Guid userID, long bookID, long monthId)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                List<Week> data = await _journDb.GetAllWeeks(userID, bookID, monthId);
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
        [HttpGet("book/journal/user={UserID}&book={BookID}/{weekId}/journal")]
        public async Task<IActionResult> GetAllBbookJournal(Guid userID, long bookID, int weekId)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                List<Journal> data = await _journDb.GetAllJournalsByWeek(userID, bookID, weekId);
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

        [HttpGet("book/journal/user={UserID}&book={BookID}/{weekId}/journal/{journalId}")]
        public async Task<IActionResult> GetJournalById(Guid userId, long bookId, int weekId, long journalId)
        {

            try
            {
                ResponseType type = ResponseType.Success;
                JournalModel data = await _journDb.GetJournalByID(userId, bookId, weekId, journalId);
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
