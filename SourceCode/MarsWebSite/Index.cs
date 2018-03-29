using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

using System.Net.Http.Headers;

namespace MarsWebSite
{
    public static class Index
    {
        [FunctionName("Index")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
            HttpRequestMessage req, TraceWriter log)
        {
            log.Info("Index request.");

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(AppSettings.IndexPage, FileMode.Open);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        [FunctionName("Site")]
        public static async Task<HttpResponseMessage> GetSiteContent([HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "Site/{file}")]
            HttpRequestMessage req, string file, TraceWriter log)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(AppSettings.SiteRoot + file, FileMode.Open);
            response.Content = new StreamContent(stream);
            string contentType = "text/html";
            string fileType = System.IO.Path.GetExtension(file).Remove(0, 1);
            if (fileType == "png"
                || fileType == "img"
                || fileType == "jpg" || fileType == "jpeg"
                || fileType == "gif"
                ) {
                contentType = "image/" + fileType;
            }
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return response;
        }
    }
}
