using BetterMomshWebAPI.EFCore;
using BetterMomshWebAPI.Utils;
using System.Text;

namespace BetterMomshWebAPI.Models
{
    public class DbHelper
    {

        private API_DataContext _DataContext;
        public DbHelper(API_DataContext DataContext)
        {

            _DataContext = DataContext;
        }


        public RegistrationModel GetUserById(int id)
        {
            RegistrationModel response = new RegistrationModel();
            var dataList = _DataContext.UserInfo.Where(d => id.Equals(id)).FirstOrDefault();
            return new RegistrationModel()
            {
                FirstName = dataList.FirstName,
                LastName = dataList.LastName,
                Occupation = dataList.Occupation,
            };

        }
        //Serves as POST/PUT/PATCH
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
                    };
                    // Generate a salt
                    UserAuth.Salt = Convert.ToBase64String(Common.GetRandomSalt(16)); // Implement salt generation as needed

                    // Hash the password using the generated salt
                    UserAuth.Password = Convert.ToBase64String(Common.SaltHashPassword(Encoding.ASCII.GetBytes(registration.password), Convert.FromBase64String(UserAuth.Salt)));

                    var utcBirthdate = DateTime.SpecifyKind(registration.Birthdate, DateTimeKind.Utc);
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

                    //establish Relationship
                    UserAuth.UserInfo = UserInfo;

                    //Add entities in database
                    _DataContext.UserCred.Add(UserAuth);
                    _DataContext.UserInfo.Add(UserInfo);
                    _DataContext.SaveChanges();
                    return "Registered Successfully";
                }
            }
            else
            {

                return "Username is Required";
            }
        }

        public string LoginUser(LoginModel login)
        {
            if (string.IsNullOrEmpty(login.username) || string.IsNullOrEmpty(login.password))
            {
                return "Username/Password is required";
            }

            var user = _DataContext.UserCred.FirstOrDefault(u => u.Username.Equals(login.username));

            if (user == null)
            {
                return "Username Doesn't Exist";
            }

            var clientPostHashPassword = Convert.ToBase64String(Common.SaltHashPassword(Encoding.ASCII.GetBytes(login.password), Convert.FromBase64String(user.Salt)));

            if (clientPostHashPassword.Equals(user.Password))
            {
                var userID = new { user.user_id };
                return "Logged In" + userID;
            }
            else
            {
                return "Wrong Password";
            }
        }
    }
}
