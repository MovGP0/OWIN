using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace HelloWorld
{
    public class StartupSimplified
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(Middleware);
        }

        public async Task Middleware(IOwinContext context)
        {
            var bytes = Encoding.UTF8.GetBytes("{ message: \"Hello World!\" }");

            var response = context.Response;
            response.ContentType = "text/json";
            response.ContentLength = bytes.Length;
            await response.WriteAsync(bytes);
        }
    }
}