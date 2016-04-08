using System;
using DryIoc;
using Microsoft.Owin.Hosting;

namespace DependencyInjection
{
    internal static class Program
    {
        private static void Main()
        {
            var c = SetupResolver();

            var startup = c.Resolve<Startup>();
            using (WebApp.Start("http://localhost:5000", startup.Configuration))
            {
                Console.WriteLine("Server started. Press enter to stop.");
                Console.ReadLine();
            }
        }

        private static IResolver SetupResolver()
        {
            var c = new Container();
            c.Register<IClock, Clock>();
            c.Register<Startup>();
            return c;
        }
    }
}
