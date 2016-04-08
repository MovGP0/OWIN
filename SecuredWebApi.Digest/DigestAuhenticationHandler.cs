using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace SecuredWebApi.Digest
{
    public sealed class DigestAuhenticationHandler : AuthenticationHandler<DigestAuthenticationOptions>
    {
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var authHeader = Request.Headers.Get("Authorization");

            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Digest", StringComparison.OrdinalIgnoreCase))
            {
                // header not available 
                return Task.FromResult(new AuthenticationTicket(null, null));
            }

            var parameter = authHeader.Substring("Digest".Length).Trim();
            string userName;

            if (!DigestAuthenticator.TryAuthenticate(parameter, Request.Method, out userName))
            {
                // coud not authenticate 
                return Task.FromResult(new AuthenticationTicket(null, null));
            }

            var identity = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Name, userName), 
                new Claim(ClaimTypes.NameIdentifier, userName) 
            }, authenticationType: "Digest");
            
            return Task.FromResult(new AuthenticationTicket(identity, null));
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode == 401)
            {
                var nonce = Options.GenerateNonceBytes().ToMD5Hash();
                Response.Headers.AppendValues("WWW-Authenticate", $"Digest realm = {Options.Realm}, nonce = {nonce}");
            }

            return Task.FromResult<object>(null);
        }
    }
}