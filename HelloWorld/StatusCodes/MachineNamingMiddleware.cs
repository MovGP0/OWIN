using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace HelloWorld
{
    public class MachineNamingMiddleware
    {
        private Func<IDictionary<string, object>, Task> Next { get; }
        
        public MachineNamingMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            Next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);

            // execute when response is sent to the client 
            // rather when Response.WriteAsync() is called 
            context.Response.OnSendingHeaders(AppendInfo, context.Response);

            await Next(env);
        }

        public void AppendInfo(object state)
        {
            var response = (OwinResponse) state;

            var statusCode = response.StatusCode;
            if (statusCode >= 400 && statusCode < 600)
            {
                response.Headers.Add("X-MachineInfo", new []
                {
                    Environment.MachineName
                });
            }
        }
    }
}