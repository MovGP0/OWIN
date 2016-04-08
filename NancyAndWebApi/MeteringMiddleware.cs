using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;
using NLog.Fluent;

namespace NancyAndWebApi
{
    public class MeteringMiddleware
    {
        private Func<IDictionary<string, object>, Task> Next { get; }

        public MeteringMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            Next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);
            var originalStream = context.Response.Body;
            var responseBuffer = new MemoryStream();
            context.Response.Body = responseBuffer;

            await Next(environment);

            var output = new
            {
                context.Request.Method, 
                context.Request.Uri, 
                responseBuffer.Length
            };

            Log.Trace().Message(JsonConvert.SerializeObject(output)).Write();

            context.Response.ContentLength = responseBuffer.Length;
            responseBuffer.Seek(0, SeekOrigin.Begin);
            await responseBuffer.CopyToAsync(originalStream);
        }
    }
}