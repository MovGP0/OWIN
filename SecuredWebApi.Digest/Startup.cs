using System.Security.Cryptography;
using System.Web.Http;
using Owin;

namespace SecuredWebApi.Digest
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // MD5 Digest Authentication 
            app.UseDigestAuthentication(new DigestAuthenticationOptions
            {
                Realm = "magical",
                GenerateNonceBytes = GenerateNonceBytes
            });

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
            app.UseWebApi(config);
        }

        public byte[] GenerateNonceBytes()
        {
            var bytes = new byte[16];
            using(var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(bytes);
            }
            return bytes;
        }
    }
}