using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using NLog.Fluent;

namespace HelloWorld
{
    public class ResponseLoggingMiddleware
    {
        private Func<IDictionary<string, object>, Task> Next { get; }
        
        public ResponseLoggingMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            Next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);

            // response stream is readonly once written
            // so we replace it with our own

            // swap output stream with buffer 
            var responseStream = context.Response.Body;
            using (var responseBuffer = new MemoryStream())
            {
                context.Response.Body = responseBuffer;

                // call next middleware
                await Next(environment);

                // spool back and read buffer 
                responseBuffer.Seek(0, SeekOrigin.Begin);
                var responseBody = await ReadAllAsync(context.Response.Body);
                Log.Trace().Message(responseBody).Write();

                // spool back and write buffer to output stream 
                responseBuffer.Seek(0, SeekOrigin.Begin);
                await responseBuffer.CopyToAsync(responseStream);
            }
        }

        public async Task<string> ReadAllAsync(Stream stream)
        {
            try
            {
                using (var reader = new StreamReader(stream))
                {
                    var content = await reader.ReadToEndAsync();
                    return content;
                }
            }
            catch (Exception e)
            {
                Log.Error().Exception(e).Write();
                return string.Empty;
            }
        }
    }
}