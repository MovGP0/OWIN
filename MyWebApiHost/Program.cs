using System;
using Microsoft.Owin.Hosting;
using MyWebApi.Controllers;

namespace MyWebApiHost
{
    public static class Program
    {
        static void Main()
        {
            // load assembly to RAM
            var controllerType = typeof (EmployeeController);

            using (WebApp.Start<Startup>("http://localhost:5000"))
            {
                Console.WriteLine("Server active. Press enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
