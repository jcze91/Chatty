using System;
using System.Security.Cryptography;

namespace BackOffice.Utils
{
    public static class Hash
    {
        public static string sha1(this string str)
        {
            var sha = new SHA1Managed();
            byte[] resultHash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(sha.Hash).Replace("-", "").ToLowerInvariant();
        }
    }
}