using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.src.Shared
{
    public class PasswordService
    {
        public static void HashPassword(string originalPassword, out string hashedPassword, out byte[] salt)
        {
            var hmac = new HMACSHA256();
            salt = hmac.Key;
            var hashedPasswordBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword));
            hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "").ToLower();
        }

        public static bool VerifyPassword(string credentialsPassword, string hashPassword, byte[] salt)
        {
            var hmac = new HMACSHA256(salt);
            var credentialsPasswordBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(credentialsPassword));
            string credentialshashedPassword = BitConverter.ToString(credentialsPasswordBytes).Replace("-", "").ToLower();
            return credentialshashedPassword.ToLower() == hashPassword.ToLower();
        }
    }
}