﻿using BetterMomshWebAPI.EFCore;
using BetterMomshWebAPI.Models;
using BetterMomshWebAPI.Models.Database_Repository;
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
        private readonly Journal_DbHelper _journDb;
        public BabyJournalController(API_DataContext _DataContext)
        {
            _db = new DbHelper(_DataContext);
            _journDb = new Journal_DbHelper(_DataContext);
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
        [HttpPut("UpJourn/{id}")]
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
            } catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("Inner Exception: " + e.InnerException?.Message);
                return BadRequest(ResponseHandler.GetExceptionResponse(e));
            }
        }
        [HttpDelete("DeleteJourn/{id}")]
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

        [HttpGet("Journal/{BookID}")]
        public IActionResult GetBbook(long BookID)
        {
            return Ok();
        }
        [HttpGet("Journal/{BookID}/{monID}")]
        public IActionResult GetBbookMon(long BookID, int monId)
        {
            return Ok();
        }
        [HttpGet("Journal/{BookID}/{monID}/{weekID}")]
        public IActionResult GetBbookWeek(long BookID, int monId, int weekID)
        {
            return Ok();
        }

        [HttpGet("Journal/{BookID}/{monId}/{weekID}/{journalID}")]
        public IActionResult GetJournal(long BookID, int monId, int weekID, long journalID)
        {
            // Your logic to retrieve and return the journal based on the provided parameters
            // ...

            return Ok(/* Your result */);
        }
    }
}
