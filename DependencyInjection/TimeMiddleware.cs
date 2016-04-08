using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace DependencyInjection
{
    public sealed class TimeMiddleware
    {
        private Func<IDictionary<string, object>, Task> Next { get; set; }
        private IClock Clock { get; }

        public TimeMiddleware(IClock clock)
        {
            Clock = clock;
        }

        public void Initialize(Func<IDictionary<string, object>, Task> next)
        {
            Next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);
            context.Response.OnSendingHeaders(AppendTimeInfo, context.Response);

            await Next.Invoke(environment);
        }

        public void AppendTimeInfo(object state)
        {
            var response = (OwinResponse) state;
            response.Headers.Append("X-Time", Clock.TimeNow.ToString("o"));
        }
    }
}