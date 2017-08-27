using System;
using System.Security.Cryptography;
using System.Text;

namespace Finance.Common.Helpers
{
    public class SecurityHelper
    {
        public static string GenerateSalt(int size = 16)
        {
            var rng = RandomNumberGenerator.Create();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        public static string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            var hashAlgorithm = SHA512.Create();
            bytes = hashAlgorithm.ComputeHash(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
