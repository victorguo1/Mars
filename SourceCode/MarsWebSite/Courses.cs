using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MarsWebSite.Components;

namespace MarsWebSite
{
    public static class Courses
    {
        [FunctionName("Courses")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", 
            Route = "courses")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("Courses Request");

            string course = (string) SimpleCache.Get("GetCourses");
            if ( course == null)
            {
                StorageService service = new StorageService();
                course = service.GetCourses();
                SimpleCache.Add("GetCourses", 10, course);
                log.Info("course refresh");
            }

            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.OK, course, "application/json");
        }
    }
}
