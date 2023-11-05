using BetterMomshWebAPI.EFCore;
using BetterMomshWebAPI.Models.Database_Repository;
using BetterMomshWebAPI.Models.JWT_Models;
using BetterMomshWebAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BetterMomshWebAPI.Models
{
    public class DbHelper : ITokenBlacklistService
    {

        private API_DataContext _DataContext;
        public DbHelper(API_DataContext DataContext)
        {

            _DataContext = DataContext;
        }
        //Serves as POST for Register User
        public string RegisterUser(RegistrationModel registration)
        {
            if (!string.IsNullOrEmpty(registration.username))
            {
                if (_DataContext.UserCred.Any(u => u.Username == registration.username))
                {
                    //if Username Already Exist
                    return "Username already exists";
                }
                else
                {
                    userCred UserAuth = new userCred
                    {
                        Username = registration.username,
                        Role = registration.Role
                    };
                    // Generate a salt
                    UserAuth.Salt = Convert.ToBase64String(Common.GetRandomSalt(16)); // Implement salt generation as needed

                    // Hash the password using the generated salt
                    UserAuth.Password = Convert.ToBase64String(Common.SaltHashPassword(Encoding.ASCII.GetBytes(registration.password), Convert.FromBase64String(UserAuth.Salt)));

                    var utcBirthdate = DateTime.SpecifyKind(registration.Birthdate.GetValueOrDefault(), DateTimeKind.Utc);

                    var UserInfo = new userInfo
                    {
                        FirstName = registration.FirstName,
                        LastName = registration.LastName,
                        MiddleName = registration.MiddleName,
                        Birthdate = utcBirthdate,
                        Religion = registration.Religion,
                        Occupation = registration.Occupation,
                        RelationshipStatus = registration.RelationshipStatus,
                        Address = registration.Address,
                        ContactNumber = registration.ContactNumber
                    };

                    var refreshToken = new RefreshTokens
                    {
                        user_id = UserAuth.user_id
                    };

                    //establish Relationship
                    UserAuth.UserInfo = UserInfo;
                    UserAuth.RefreshTokens = refreshToken;

                    //Add entities in database
                    _DataContext.UserCred.Add(UserAuth);
                    _DataContext.UserInfo.Add(UserInfo);
                    _DataContext.RefreshTokens.Add(refreshToken);
                    _DataContext.SaveChanges();
                    return "Registered Successfully";
                }
            }
            else
            {

                return "Username is Required";
            }
        }

        //POST Login User
        public UserModel Authenticate(LoginModel login)
        {
            var usernameLower = login.username?.ToLower();
            var user = _DataContext.UserCred.FirstOrDefault(o => usernameLower != null && o.Username.ToLower() == usernameLower);
            var userInfo = _DataContext.UserInfo.FirstOrDefault(o => o.user_id.Equals(user.user_id));

            if (user == null)
            {
                // User not found
                return null;
            }

            var clientPostHashPassword = Convert.ToBase64String(Common.SaltHashPassword(Encoding.ASCII.GetBytes(login.password), Convert.FromBase64String(user.Salt)));

            if (clientPostHashPassword.Equals(user.Password))
            {
                UserModel userModel = new UserModel
                {
                    user_id = userInfo.user_id,
                    username = user.Username,
                    Role = user.Role,
                    FirstName = userInfo.FirstName,
                    MiddleName = userInfo.MiddleName,
                    LastName = userInfo.LastName,
                    Birthdate = userInfo.Birthdate,
                    ContactNumber = userInfo.ContactNumber,
                    Address = userInfo.Address,
                    Occupation = userInfo.Occupation,
                    Religion = userInfo.Religion,
                    RelationshipStatus = userInfo.RelationshipStatus
                    // Map other properties
                };

                return userModel; // Assuming that your RegistrationModel matches the user object
            }
            else
            {
                // Wrong password
                return null;
            }
        }

        //
        //
        //
        //Baby Book Controller (Journals, Creation of BabyBook)
        //in every baby book creates 3 trimester data
        //in every trimester creates 3 month data
        //
        public string CreateBabyBook(BabyBookModel babyBook)
        {
            var user = _DataContext.UserCred.FirstOrDefault(u => u.user_id.Equals(babyBook.user_id));

            if (user == null)
            {
                return "User Doesn't Exist";
            }
            else
            {
                if (!string.IsNullOrEmpty(babyBook.Title))
                {
                    BabyBook bBook = new BabyBook
                    {
                        Title = babyBook.Title,
                        Created = DateOnly.FromDateTime(DateTime.Now),
                        user_id = babyBook.user_id
                    };
                    // Add the BabyBook entity to the database
                    _DataContext.BabyBook.Add(bBook);
                    _DataContext.SaveChanges();

                    // Create and associate trimesters with unique IDs
                    for (int i = 1; i <= 3; i++)
                    {
                        Trimester trimester = new Trimester
                        {
                            BookId = bBook.BookId, // Set the foreign key to associate with the BabyBook
                            Trimesters = i + " Trimester",
                        };
                        // Add the trimester to the database
                        _DataContext.trimesters.Add(trimester);
                    }
                    _DataContext.SaveChanges();

                    // Create months associated with each trimester
                    foreach (var trimester in _DataContext.trimesters.Where(t => t.BookId == bBook.BookId))
                    {
                        for (int j = 1; j <= 3; j++)
                        {
                            Months month = new Months
                            {
                                Month = "Month " + j,
                                TrimesterId = trimester.TrimesterId
                            };
                            _DataContext.months.Add(month);
                        }
                    }
                    _DataContext.SaveChanges();
                    return "Baby Book Added";
                }
                else
                {
                    return "Title is Required";
                }
            }
        }

        //
        //
        //
        //Adding Journal Components
        //
        //
        //
        public string AddJournal(JournalModel journal)
        {
            var monthid = _DataContext.months.FirstOrDefault(u => u.MonthId.Equals(journal.MonthId));
            var bookid = _DataContext.BabyBook.FirstOrDefault(u => u.BookId.Equals(journal.BookId));
            DateTime localDateTime = DateTime.Now; // Your local DateTime
            DateTime utcDateTime = localDateTime.ToUniversalTime(); // Convert to UTC
            if (bookid == null || monthid == null)
            {
                return "Book Data doesn't exist";
            }
            else
            {
                Journal journ = new Journal
                {
                    JournalName = journal.JournalName,
                    journalEntry = journal.JournalEntry,
                    Entry_Date = utcDateTime,
                    PhotoData = journal.PhotoData,
                    BookId = journal.BookId,
                    //MonthId = journal.MonthId
                };
                _DataContext.journal.Add(journ);
                _DataContext.SaveChanges();
                return "Journal Added";
            }
        }
        public BabyBookModel GetBbookById(int id)
        {
            BabyBookModel response = new BabyBookModel();
            var dataList = _DataContext.BabyBook.Where(d => id.Equals(id)).FirstOrDefault();
            return new BabyBookModel()
            {
                Title = dataList.Title,
                Created = dataList.Created,
                user_id = dataList.user_id
            };
        }

        public Task<RefreshTokens> GetByToken(string token)
        {
            var refreshToken = _DataContext.RefreshTokens.FirstOrDefault(r => r.RefreshToken == token);
            return Task.FromResult(refreshToken);
        }

        public async Task<bool> UpdateRefreshToken(Guid? userId, RefreshToken newRefreshToken)
        {
            try
            {
                var user = _DataContext.RefreshTokens.FirstOrDefault(u => u.user_id == userId);

                if (user != null)
                {
                    user.RefreshToken = newRefreshToken.Token;
                    user.TokenCreated = newRefreshToken.Created.ToUniversalTime();
                    user.TokenExpired = newRefreshToken.Expires.ToUniversalTime();

                    await _DataContext.SaveChangesAsync();
                    return true; // Refresh token updated successfully
                }

                return false; // User not found
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                Console.WriteLine("Error: " + ex);
                return false; // Failed to update the refresh token
            }
        }


        public async Task AddToBlacklistAsync(string token)
        {
            var entity = new TokenBlacklist { Token = token };
            _DataContext.blacklist.Add(entity);
            await _DataContext.SaveChangesAsync();
        }
        public async Task<bool> IsTokenBlacklistedAsync(string token)
        {
            return await _DataContext.blacklist.AnyAsync(t => t.Token == token);
        }

        public async Task<UserModel> getUserID(Guid? userId)
        {
            var user = _DataContext.UserCred.FirstOrDefault(u => u.user_id == userId);
            if (user == null)
            {
                return null;
            }
            else
            {
                return new UserModel()
                {
                    user_id = user.user_id

                };
            }
        }

    }
}
