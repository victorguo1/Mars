using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace MarsWebSite
{
    public static class Services
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="name">Container name</param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("Services")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Services/name/{name}")]HttpRequestMessage req, string name, TraceWriter log)
        {
            // if user is authenticated and authorized, otherwise return nothing
            log.Info("C# HTTP trigger function processed a request.");

            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.OK, "Hello " + name + "!!");
        }
        
    }
}
