using System;
using HelloWorld;
using Microsoft.Owin.Hosting;

namespace ConsoleHost
{
    public class Program
    {
        public static void Main()
        {
            using (WebApp.Start<Startup>("http://localhost:5000"))
            {
                Console.WriteLine("Server active");
                Console.ReadLine();
            }
        }

    }
}
