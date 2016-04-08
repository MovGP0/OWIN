using System.Text.RegularExpressions;

namespace SecuredWebApi.Digest
{
    public static class DigestAuthenticator
    {
        public static bool TryAuthenticate(string headerParameter, string method, out string userName)
        {
            const string pattern = @"(\w+)=""([^""\\]*)""\s*(?:,\s*|$)";

            string realm = null;
            string nonce = null;
            string uri = null;
            string response = null;
            string user = null;

            var replacedString = Regex.Replace(headerParameter, pattern, match => ReplaceMatch(match, ref realm, ref nonce, ref uri, ref response, ref user));

            if (realm == null || nonce == null || uri == null || response == null || user == null)
            {
                userName = null;
                return false;
            }

            var password = user; // do not use in production
            var ha1 = $"{user}:{realm}:{password}".ToMD5Hash();
            var ha2 = $"{method}:{uri}".ToMD5Hash();
            var computedResponse = $"{ha1}:{nonce}:{ha2}".ToMD5Hash();

            userName = user;
            return string.CompareOrdinal(response, computedResponse) == 0;
        }

        public static string ReplaceMatch(Match match, ref string realm, ref string nonce, ref string uri, ref string response, ref string user)
        {
            var key = match.Groups[1].Value.Trim();
            var value = match.Groups[2].Value.Trim();

            switch (key)
            {
                case "username":
                    user = value;
                    break;
                case "realm":
                    realm = value;
                    break;
                case "nonce":
                    nonce = value;
                    break;
                case "uri":
                    uri = value;
                    break;
                case "response":
                    response = value;
                    break;
            }

            return string.Empty;
        }
    }
}