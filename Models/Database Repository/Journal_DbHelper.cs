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
                var journal = _DataContext.journal.FirstOrDefault(u => u.journalId == journalID);

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
                var journal = _DataContext.journal.FirstOrDefault(u => u.journalId == id);

                if (journal != null)
                {
                    _DataContext.journal.Remove(journal);
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
                    .Where(j => j.user_id == UserID)
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
        public async Task<List<Trimester>> GetAllTrimesters(long BabybookID)
        {

            try
            {
                var BabyBookTrimesters = await _DataContext.trimesters
                    .Where(j => j.BookId == BabybookID)
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
        public async Task<List<Months>> GetAllMonths(long TrimesterID, long MonthID)
        {

            try
            {
                var TrimesterMonths = await _DataContext.months
                    .Where(j => j.MonthId == MonthID && j.TrimesterId == TrimesterID)
                    .Select(j => new Months
                    {
                        MonthId = j.MonthId,
                        Month = j.Month,
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
        public async Task<List<Weeks>> GetAllWeeks(long WeekID)
        {

            try
            {
                var BabyBookTrimesters = await _DataContext.week
                    .Where(j => j.weekId == WeekID)
                    .Select(j => new Weeks
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
        public async Task<List<Journal>> GetAllJournalsFromWeek(long WeekID)
        {
            try
            {
                var journals = _DataContext.journal
                    .Where(j => j.weekId == WeekID)
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

        public async Task<List<Journal>> GetAllJournals(long BookID)
        {
            try
            {
                var journals = _DataContext.journal
                    .Where(j => j.BookId == BookID)
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
        public async Task<JournalModel> GetJournalByID(long journalID)
        {
            try
            {
                var journal = await _DataContext.journal
                    .Where(j => j.journalId == journalID)
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
