using System.Security.Cryptography;
using Owin;
using SecuredWebApi.Digest;

namespace MyWebApi.IISHost
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseDigestAuthentication(new DigestAuthenticationOptions
            {
                Realm = "magical", 
                GenerateNonceBytes = GenerateNonceBytes
            });
        }

        private static byte[] GenerateNonceBytes()
        {
            var bytes = new byte[16];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(bytes);
            }
            return bytes;
        }
    }
}
