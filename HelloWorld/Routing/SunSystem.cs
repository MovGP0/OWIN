using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace HelloWorld
{
    public class SunSystem
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/planet", helloApp =>
            {
                helloApp.Map("/1", a =>
                {
                    a.Use(Mercury);
                    a.Run(End);
                });

                helloApp.Map("/2", a =>
                {
                    a.Use(Venus);
                    a.Run(End);
                });

                helloApp.Map("/3", a =>
                {
                    a.Use(Earth);
                    a.Run(End);
                });

                helloApp.MapWhen(IsNoPlanet, a => a.Run(NoPlanet));

                helloApp.Use(Mercury);
                helloApp.Use(Venus);
                helloApp.Use(Earth);
                helloApp.Run(End);
            });

            app.Use(Universe);
            app.Run(End);
        }

        public bool IsNoPlanet(IOwinContext context)
        {
            if(!context.Request.Path.HasValue)
                return false;

            int planetNumber;
            if (!int.TryParse(context.Request.Path.Value.Trim('/'), out planetNumber))
            {
                return false;
            }

            if (planetNumber > 8)
            {
                return true;
            }

            return false;
        }

        public async Task NoPlanet(IOwinContext context)
        {
            await context.Response.WriteAsync("outside of sun system");
        }

        public async Task Mercury(IOwinContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("Mercury");
            await next.Invoke();
        }

        public async Task Venus(IOwinContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("Venus");
            await next.Invoke();
        }

        public async Task Earth(IOwinContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("Earth");
            await next.Invoke();
        }

        public async Task Universe(IOwinContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("Universe");
            await next.Invoke();
        }

        public async Task End(IOwinContext context)
        {
            await context.Response.WriteAsync(string.Empty);
        }
    }
}