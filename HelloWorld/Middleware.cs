using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    public class Middleware
    {
        private Func<IDictionary<string, object>, Task> Next { get; }

        public Middleware(Func<IDictionary<string, object>, Task> next)
        {
            Next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var bytes = Encoding.UTF8.GetBytes("Hello World!");
            var response = (Stream)env["owin.ResponseBody"];
            await response.WriteAsync(bytes, 0, bytes.Length);
            await Next(env);
        }
    }
}