using System;
using System.Security.Cryptography;

namespace SecuredWebApi
{
    public static class KeyFactory
    {
        public static byte[] Create()
        {
            var secretKeyBytes = new byte[32];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(secretKeyBytes);
            }
            return secretKeyBytes;
        }

        public static string CreateBase64()
        {
            return Convert.ToBase64String(Create());
        }
    }
}