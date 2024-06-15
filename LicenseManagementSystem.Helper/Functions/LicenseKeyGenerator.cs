using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Helper.Functions
{
    public static class LicenseKeyGenerator
    {
        public static string GenerateLicenseKey(int length = 16)
        {
            if (length <= 0)
                throw new ArgumentException("License key length must be a positive number.");
            var guid = Guid.NewGuid();
            var guidBytes = guid.ToByteArray();
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(guidBytes);
            var licenseKey = Convert.ToBase64String(hashBytes)
                .Replace("=", "")
                .Replace("+", "")
                .Replace("/", "")
                .Substring(0, length);
            return licenseKey;
        }
    }
}
