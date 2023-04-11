using System.Security.Cryptography;
using System.Text;

namespace testTask.Data.Encoding
{
    public class HashPasswordHelper
    {
        public static string GetHashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var hashedBytes = sha.ComputeHash(UTF7Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-","").ToLower();
                return hash;
            }
        }
    }
}
