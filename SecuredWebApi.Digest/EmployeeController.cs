using System.Net.Http;
using System.Web.Http;

namespace SecuredWebApi.Digest
{
    public class EmployeeController : ApiController
    {
        [Authorize]
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse("Hello WebApi!");
        }
    }
}