using System.Text;
using System.Security.Cryptography;
using SmartBusAPI.Common.Interfaces.Services;

namespace SmartBusAPI.Services
{
    public class HashProviderService : IHashProviderService
    {
        public string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public bool VerifyHash(string input, string hash)
        {
            string computedHash = ComputeHash(input);
            return hash.Equals(computedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}