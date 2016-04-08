using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace HelloWorld
{
    public class ParameterizedMiddleware
    {
        private Func<IDictionary<string, object>, Task> Next { get; }
        private GreetingOptions Options { get; }

        public ParameterizedMiddleware(Func<IDictionary<string, object>, Task> next, GreetingOptions options)
        {
            Next = next;
            Options = options;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);
            var message = Options.IsHtml
                ? $"<h1>{Options.Message}</h1>"
                : Options.Message;

            var headers = (IDictionary<string, string[]>) env["owin.ResponseHeaders"];
            headers["Content-Type"] = new [] {"text/html"};

            var bytes = Encoding.UTF8.GetBytes(message);
            await context.Response.WriteAsync(bytes);
            await Next(env);
        }
    }
}