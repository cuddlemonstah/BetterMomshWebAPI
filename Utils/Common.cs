using System.Security.Cryptography;

namespace BetterMomshWebAPI.Utils
{
    public class Common
    {
        /*
       * 
       * Create Random Salt String
       * */

        public static byte[] GetRandomSalt(int length)
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[length];
            rng.GetNonZeroBytes(salt);
            return salt;
        }

        /*
         * 
         * Create Password with salt
         * */

        public static byte[] SaltHashPassword(byte[] password, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] plainTextWithSaltBytes = new byte[password.Length + salt.Length];
            for (int i = 0; i < password.Length; i++)
                plainTextWithSaltBytes[i] = password[i];
            for (int i = 0; i < salt.Length; i++)
                plainTextWithSaltBytes[password.Length + i] = salt[i];
            return algorithm.ComputeHash(plainTextWithSaltBytes);

        }
        public DateTime? ParseBirthdate(string birthdateClaim)
        {
            if (!string.IsNullOrEmpty(birthdateClaim) && DateTime.TryParse(birthdateClaim, out DateTime birthdate))
            {
                return birthdate;
            }
            return null; // Return null if the birthdate claim is missing or invalid
        }
        public decimal? ParseContactNumber(string contactClaim)
        {
            if (!string.IsNullOrEmpty(contactClaim) && decimal.TryParse(contactClaim, out decimal contactNum))
            {
                return contactNum;
            }
            return null; // Return null if the birthdate claim is missing or invalid
        }

        public Guid? ParseId(string user_id)
        {
            if (!string.IsNullOrEmpty(user_id) && Guid.TryParse(user_id, out Guid userId))
            {
                // If the string is successfully parsed to a Guid, assign it to the user_id
                return userId;
            }
            return null;
        }
    }
}
