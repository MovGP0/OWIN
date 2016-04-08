using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using NLog.Fluent;

namespace HelloWorld
{
    public class RequestLoggingMiddleware
    {
        private Func<IDictionary<string, object>, Task> Next { get; }
        
        public RequestLoggingMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            Next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);

            var requestBuffer = new MemoryStream();
            await context.Request.Body.CopyToAsync(requestBuffer);

            requestBuffer.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBuffer;

            using (var reader = new StreamReader(context.Request.Body))
            {
                // when reading the body stream to end, it stays there
                var body = await reader.ReadToEndAsync();
                Log.Trace().Message(body).Write();
            }

            // spool stream back to the beginning 
            context.Request.Body.Seek(0, SeekOrigin.Begin);

            await Next(environment);
        }
    }
}