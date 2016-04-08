using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin;
using Owin;

namespace MyWebApi.IISHost
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapOwinPath("/rss", app => app.Run(RssReader));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        private static Task RssReader(IOwinContext context)
        {
            context.Response.ContentType = "application/rss+xml";

            var response = new StringBuilder()
                .AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8"" ?>")
                .AppendLine(@"<rss version=""2.0"">")
                .AppendLine(@"  <channel>")
                .AppendLine(@"    <title>My RSS Feed</title>")
                .AppendLine(@"    <item>")
                .AppendLine(@"      <title>Hello World!</title>")
                .AppendLine(@"      <description>RSS Feed Example</description>")
                .AppendLine(@"    </item>")
                .AppendLine(@"  </channel>")
                .AppendLine(@"</rss>")
                .ToString();

            return context.Response.WriteAsync(response);
        }
    }
}
