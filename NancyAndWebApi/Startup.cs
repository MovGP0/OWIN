using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace NancyAndWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<MeteringMiddleware>();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)), 
                RequestPath = new PathString("/files")
            });

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
            app.UseWebApi(config);

            app.UseNancy();
        }
    }
}