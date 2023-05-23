using System;
using System.Security.Cryptography;
using System.Text;
using InventoryServer.Common.Extensions;

namespace InventoryServer.Services.Crypt
{
    public class CryptSHA256Service : ICryptService
    {
        public string ComputeHash(string text)
        {
            if (text.IsNullOrEmpty())
                return null;

            using var sha256 = new SHA256Managed();
            return BitConverter.ToString(sha256.ComputeHash(Encoding.ASCII.GetBytes(text))).Replace("-", "");
        }
    }
}
