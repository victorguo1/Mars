using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MarsWebSite.Components;

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
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Services/{name}")]HttpRequestMessage req, string name, TraceWriter log)
        {
            // if user is authenticated and authorized, otherwise return nothing
            log.Info("C# HTTP trigger function processed a request.");
            try
            {
                string user = System.Security.Claims.ClaimsPrincipal.Current.Identity.Name;
                log.Info(user);
            }
            catch {
                //
                log.Warning("can't get user identity or it's an anonymous user.");
            }

            StorageService service = new StorageService();
            string list = service.ListFilesOrDirectories(name.Replace("-","/"));
            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.OK, list, "application/json");
        }       
    }
}
