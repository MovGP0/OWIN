using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SecuredWebApi.Digest
{
    public static class HashHelper
    {
        public static string ToMD5Hash(this byte[] bytes)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(bytes)
                    .Aggregate(new StringBuilder(), (sb, b) => sb.Append($"{b:x2}"), sb => sb.ToString());
            }
        }

        public static string ToMD5Hash(this string @string)
        {
            return ToMD5Hash(Encoding.UTF8.GetBytes(@string));
        }
    }
}