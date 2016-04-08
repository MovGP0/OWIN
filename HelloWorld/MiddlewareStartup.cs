using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace HelloWorld
{
    public class MiddlewareStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<Middleware>();
            app.Use<MachineNamingMiddleware>();
            app.Use<ErrorProducingMiddleware>();
            app.Use<ParameterizedMiddleware>(new GreetingOptions { IsHtml = true, Message = "Foo" });
            app.Run(End);
        }

        public async Task End(IOwinContext context)
        {
            await context.Response.WriteAsync(string.Empty);
        }
    }
}