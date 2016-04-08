using System;
using Microsoft.Owin.Hosting;

namespace SecuredWebApi
{
    public static class Program
    {
        public static void Main()
        {
            using (WebApp.Start<Startup>("http://localhost:5000"))
            {
                Console.WriteLine("Server started. Press enter to stop.");
                Console.ReadLine();
            }
        }
    }
}
