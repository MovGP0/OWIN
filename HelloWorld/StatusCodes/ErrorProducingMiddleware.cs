using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace HelloWorld
{
    public class ErrorProducingMiddleware
    {
        private Func<IDictionary<string, object>, Task> Next { get; }
        
        public ErrorProducingMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            Next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);
            await Next.Invoke(env);
            context.Response.StatusCode = 404;
        }
    }
}