using System;
using Microsoft.Owin.Security;

namespace SecuredWebApi.Digest
{
    public sealed class DigestAuthenticationOptions : AuthenticationOptions
    {
        public DigestAuthenticationOptions() : base("Digest")
        {
        }

        public string Realm { get; set; }
        public Func<byte[]> GenerateNonceBytes { get; set; }
    }
}