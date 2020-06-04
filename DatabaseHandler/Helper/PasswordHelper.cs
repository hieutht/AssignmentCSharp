using System;
using System.Security.Cryptography;
using System.Text;

namespace DatabaseHandler.Helper
{
    public class PasswordHelper
    {
        private static readonly int _saltSize = 7;
        // Sinh ra chuỗi random gồm 7 ký tự.
        public static string GenerateSalt()
        {
            var builder = new StringBuilder();    
            var random = new Random();    
            char ch;    
            for (int i = 0; i < _saltSize; i++)    
            {    
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));    
                builder.Append(ch);    
            }
            return builder.ToString();    
        }    
        public static string EncryptString(string input)
        {
            var hash = new StringBuilder();
            var md5provider = new MD5CryptoServiceProvider();
            var bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
            foreach (var t in bytes)
            {
                hash.Append(t.ToString("x2"));
            }
            return hash.ToString();
        }

        // so sánh mật khẩu người dùng nhập vào + (mã hoá) với mật khẩu đã mã hoá trong db. 
        public static bool ComparePassword(string inputPassword, string encryptPassword, string salt)
        {
            return EncryptString(inputPassword + salt) == encryptPassword;
        }
    }
}