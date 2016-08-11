using System;
using System.Security.Cryptography;
using System.Text;

namespace WebAutoLogin.Security.Cryptography
{
    public class DefaultHashService : IHashService
    {
        public string Hash(string clearText)
        {
            var bytes = Encoding.Unicode.GetBytes(clearText);
            var sha256Managed = new SHA256Managed();
            var hashBytes = sha256Managed.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            var hash = sb.ToString();
            return hash;
        }
    }
}