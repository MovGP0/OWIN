using Nancy;

namespace NancyAndWebApi
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = x => "Hello Nancy!";
        }
    }
}