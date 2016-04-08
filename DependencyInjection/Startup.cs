using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace DependencyInjection
{
    public sealed class Startup
    {
        private IClock Clock { get; }

        public Startup(IClock clock)
        {
            Clock = clock;
        }

        public void Configuration(IAppBuilder app)
        {
            app.Use(new TimeMiddleware(Clock));
            app.Run(Greet);
        }

        private static async Task Greet(IOwinContext context)
        {
            await context.Response.WriteAsync("Hello World!");
        }
    }
}