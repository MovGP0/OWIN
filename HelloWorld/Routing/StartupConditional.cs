using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace HelloWorld
{
    public class StartupConditional
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/hello", helloApp =>
            {
                helloApp.Use(HelloWorld);
                helloApp.Run(End);
            });

            app.Use(Hello);
            app.Use(World);
            app.Run(End);
        }

        public async Task HelloWorld(IOwinContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("Hello World!");
            await Task.Factory.FromAsync<IAsyncResult>(next.BeginInvoke, next.EndInvoke, null);
        }

        public async Task Hello(IOwinContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("Hello ");
            await Task.Factory.FromAsync<IAsyncResult>(next.BeginInvoke, next.EndInvoke, null);
        }

        public async Task World(IOwinContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("World!");
            await Task.Factory.FromAsync<IAsyncResult>(next.BeginInvoke, next.EndInvoke, null);
        }

        public async Task End(IOwinContext context)
        {
            await context.Response.WriteAsync(string.Empty);
        }
    }
}