using System.Web.Http;
using Microsoft.Owin.Security.Jwt;
using Owin;

namespace SecuredWebApi
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // JSON Web Token Authentication
            const string key = "ZzXLrqfHwpYY0snL2+0FagmGX+4FrnwO5CrP51YCFxc="; // KeyFactory.CreateBase64();
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AllowedAudiences = new[] { "http://localhost:5000/api" },
                IssuerSecurityTokenProviders = new[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(
                        issuer: "http://authzserver.demo", // trusted authority
                        base64Key: key // public key to validate token
                        )
                }
            });
            
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
            app.UseWebApi(config);
        }
    }
}