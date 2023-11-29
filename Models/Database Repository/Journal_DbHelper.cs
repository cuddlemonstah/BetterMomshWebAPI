using BetterMomshWebAPI.EFCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BetterMomshWebAPI.Models.Database_Repository
{
    [Authorize]
    public class Journal_DbHelper
    {
        private readonly API_DataContext _DataContext;

        public Journal_DbHelper(API_DataContext _db)
        {
            _DataContext = _db;
        }


        public async Task<JournalModel> UpdateJournal(JournalModel model, long journalID)
        {
            try
            {
                var journal = _DataContext.Journal.FirstOrDefault(u => u.journalId == journalID);

                if (journal != null)
                {
                    journal.JournalName = model.JournalName;
                    journal.journalEntry = model.JournalEntry;
                    journal.PhotoData = model.PhotoData;

                    await _DataContext.SaveChangesAsync();
                    return new JournalModel
                    {
                        JournalName = journal.JournalName,
                        JournalEntry = journal.journalEntry,
                        PhotoData = journal.PhotoData,
                        // Include other properties as needed
                    }; // Refresh token updated successfully
                }

                return null; // User not found
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine("Error: " + ex);
                return null; // Failed to update the refresh token
            }
        }

        public async Task<bool> DeleteJournal(long id)
        {
            try
            {
                var journal = _DataContext.Journal.FirstOrDefault(u => u.journalId == id);

                if (journal != null)
                {
                    _DataContext.Journal.Remove(journal);
                    await _DataContext.SaveChangesAsync();

                    return true; // Journal deleted successfully
                }

                return false; // Journal not found
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine("Error: " + ex);
                return false; // Failed to delete the journal
            }
        }
        public async Task<List<BabyBook>> GetAllBabyBooks(Guid UserID)
        {
            try
            {
                var BabyBooks = await _DataContext.BabyBook
                    .Where(j => j.user_Id == UserID)
                    .Select(j => new BabyBook
                    {
                        BookId = j.BookId,
                        Title = j.Title,
                        Created = j.Created
                        // Map other properties as needed
                    })
                    .ToListAsync();

                return BabyBooks;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine("Error: " + ex);
                return null; // Failed to get journals
            }
        }
        public async Task<List<Trimester>> GetAllTrimesters(Guid userId, long babybookID)
        {

            try
            {
                var BabyBookTrimesters = await _DataContext.Trimester
                    .Where(j => j.BookId == babybookID && j.user_id == userId)
                    .Select(j => new Trimester
                    {
                        TrimesterId = j.TrimesterId,
                        Trimesters = j.Trimesters
                        // Map other properties as needed
                    })
                    .ToListAsync();

                return BabyBookTrimesters;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine("Error: " + ex);
                return null; // Failed to get journals
            }
        }
        public async Task<List<Month>> GetAllMonths(Guid userId, long TrimesterID, long bookId)
        {

            try
            {
                var TrimesterMonths = await _DataContext.Month
                    .Where(j => j.TrimesterId == TrimesterID && j.user_id == userId && j.BookId == bookId)
                    .Select(j => new Month
                    {
                        MonthId = j.MonthId,
                        Months = j.Months,
                        // Map other properties as needed
                    })
                    .ToListAsync();

                return TrimesterMonths;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine("Error: " + ex);
                return null; // Failed to get journals
            }
        }
        public async Task<List<Week>> GetAllWeeks(Guid userID, long bookID, long monthId)
        {

            try
            {
                var BabyBookTrimesters = await _DataContext.Week
                    .Where(j => j.MonthId == monthId && j.BookId == bookID && j.user_id == userID)
                    .Select(j => new Week
                    {
                        weekId = j.weekId,
                        week_number = j.week_number
                        // Map other properties as needed
                    })
                    .ToListAsync();

                return BabyBookTrimesters;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine("Error: " + ex);
                return null; // Failed to get journals
            }
        }
        public async Task<List<Journal>> GetAllJournalsByWeek(Guid userID, long bookID, long weekId)
        {
            try
            {
                var journals = _DataContext.Journal
                    .Where(j => j.weekId == weekId && j.BookId == bookID && j.user_id == userID)
                    .Select(j => new Journal
                    {
                        journalId = j.journalId,
                        JournalName = j.JournalName,
                        journalEntry = j.journalEntry,
                        PhotoData = j.PhotoData
                        // Map other properties as needed
                    })
                    .ToListAsync();

                return await journals;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine("Error: " + ex);
                return null; // Failed to get journals
            }
        }

        public async Task<JournalModel> GetJournalByID(Guid userId, long bookId, long weekId, long journalID)
        {
            try
            {
                var journal = await _DataContext.Journal
                    .Where(j => j.journalId == journalID && j.user_id == userId && j.BookId == bookId && j.weekId == weekId)
                    .Select(j => new JournalModel
                    {
                        JournalName = j.JournalName,
                        JournalEntry = j.journalEntry,
                        PhotoData = j.PhotoData
                        // Map other properties as needed
                    })
                    .FirstOrDefaultAsync();

                return journal;
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine("Error: " + ex);
                return null; // Failed to get the journal
            }
        }
    }
}
