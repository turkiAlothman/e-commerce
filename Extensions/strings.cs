using System.Security.Cryptography;
using System.Text;

namespace e_commerce.Extensions
{
    public static class strings
    {
        public static string Hash(this string input){
            SHA256 function = SHA256.Create();
            var byted = Encoding.UTF8.GetBytes(input);
            var hashedBytes = function.ComputeHash(byted);
            return Convert.ToBase64String(hashedBytes);
        }
    }
}