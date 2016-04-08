using System;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;

namespace HelloWorld
{
    public class StartupChaining
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(Middleware1);
            app.Run(Middleware2);
        }

        public async Task Middleware1(IOwinContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("Hello ");
            await Task.Factory.FromAsync<IAsyncResult>(next.BeginInvoke, next.EndInvoke, null);
            await context.Response.WriteAsync("!!!");
        }

        public async Task Middleware2(IOwinContext context)
        {
            await context.Response.WriteAsync("World");
        }
    }
}