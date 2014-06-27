using System;
using System.Security.Cryptography;
using System.Text;

namespace Chatty.Utils
{
    public static class Hash
    {
        public static string sha1(this string str)
        {
            var sha = new SHA1Managed();
            byte[] resultHash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(sha.Hash).Replace("-", "").ToLowerInvariant();
        }

        public static byte[] GetBytes(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
