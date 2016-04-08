using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Owin;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace HelloWorld
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var middleware = new Func<AppFunc, AppFunc>(Middleware);
            app.Use(middleware);
        }

        public AppFunc Middleware(AppFunc nextMiddleware)
        {
            AppFunc appFunc = env =>
            {
                var bytes = Encoding.UTF8.GetBytes("{ message: \"Hello World!\" }");
                var headers = (IDictionary<string, string[]>) env["owin.ResponseHeaders"];
                headers["Content-Length"] = new[] {bytes.Length.ToString()};
                headers["Content-Type"] = new[] {"text/json"};

                var response = (Stream) env["owin.ResponseBody"];
                response.WriteAsync(bytes, 0, bytes.Length);
                return nextMiddleware(env);
            };
            return appFunc;
        }
    }
}
