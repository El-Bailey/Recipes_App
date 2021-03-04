using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Recipes_App
{
    class PasswordEncryptionUsingRFC2898
    {
        // Compare hash for password entered by user with password hash in the DB.
        public static bool CheckPassword(string plaintextPassword, string passwordHashFromDB)
        {
            bool isMatch = false;

            var splitPwHash = passwordHashFromDB.Split('|');

            if(splitPwHash.Length == 3)
            {
                string pwHashToCompare = GetPasswordHash(plaintextPassword, Convert.ToInt32(splitPwHash[0]), splitPwHash[1]);

                if (passwordHashFromDB == pwHashToCompare)
                {
                    isMatch = true;
                }
            }

            return isMatch;
        }

        // Generate password hash for new password
        public static string GetPasswordHash(string plaintextPassword)
        {
            string passwordHash = string.Empty;


            if (plaintextPassword.Length == 0)
            {
                return passwordHash;
            }
            else
            {
                // Create a byte array to hold the random value.
                byte[] salt = new byte[24];
                using (RNGCryptoServiceProvider rngCSP = new RNGCryptoServiceProvider())
                {
                    // Fill the array with a random value.
                    rngCSP.GetBytes(salt);
                }

                int iterations = 1000;

                // Get Hash
                Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(plaintextPassword, salt, iterations);

                passwordHash = iterations + "|" + Convert.ToBase64String(salt) + "|" + Convert.ToBase64String(hash.GetBytes(128));

                return passwordHash;
            }


        }

        public static string GetPasswordHash(string plaintextPassword, int iterations, string salt)
        {
            string passwordHash = string.Empty;

            byte[] saltByteArray = Convert.FromBase64String(salt);

            if (plaintextPassword.Length == 0)
            {
                return passwordHash;
            }
            else
            {
                // Get Hash
                Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(plaintextPassword, saltByteArray, iterations);

                passwordHash = iterations + "|" + Convert.ToBase64String(saltByteArray) + "|" + Convert.ToBase64String(hash.GetBytes(128));

                return passwordHash;
            }


        }
    }
}
