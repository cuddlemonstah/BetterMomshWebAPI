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

    }
}
