using Microsoft.Owin.Extensions;
using Owin;

namespace SecuredWebApi.Digest
{
    public static class DigestAuthenticationMiddlewareExtensions
    {
        public static IAppBuilder UseDigestAuthentication(this IAppBuilder app, DigestAuthenticationOptions options)
        {
            app.Use<DigestAuthenticationMiddleware>(options);
            app.UseStageMarker(PipelineStage.Authenticate);
            return app;
        }
    }
}