using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;

namespace SecuredWebApi.Digest
{
    public sealed class DigestAuthenticationMiddleware : AuthenticationMiddleware<DigestAuthenticationOptions>
    {
        public DigestAuthenticationMiddleware(OwinMiddleware next, DigestAuthenticationOptions options) : base(next, options)
        {
        }

        protected override AuthenticationHandler<DigestAuthenticationOptions> CreateHandler()
        {
            return new DigestAuhenticationHandler();
        }
    }
}