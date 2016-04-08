using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace HelloWorld
{
    public class EchoMiddleware
    {
        private Func<IDictionary<string, object>, Task> Next { get; }
        
        public EchoMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            Next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);
            
            string body;
            using (var reader = new StreamReader(context.Request.Body))
            {
                // when reading the body, thge body gets deleted
                body = await reader.ReadToEndAsync();
            }
            
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(body);
            await context.Response.WriteAsync(body);
        }
    }
}