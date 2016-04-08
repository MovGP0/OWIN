using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            if (id > 10 || id < 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var employee = new Employee
            {
                Id = id,
                FirstName = "Jane",
                LastName = "Doe"
            };

            return Request.CreateResponse(employee);
        }
    }
}
