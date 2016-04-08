using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

namespace MyWebApiHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(CreateWebApiConfiguration());

            app.Run(HelloWorld);
        }

        public HttpConfiguration CreateWebApiConfiguration()
        {
            var config = new HttpConfiguration();
            ConfigureRoutes(config);
            return config;
        }

        private static void ConfigureRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
        }

        public async Task HelloWorld(IOwinContext context)
        {
            var bytes = Encoding.UTF8.GetBytes("Hello World!");
            context.Response.ContentLength = bytes.Length;
            await context.Response.WriteAsync(bytes);
        }
    }
}
