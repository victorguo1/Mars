using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MarsWebSite.Components;
using System.Net.Http.Headers;
using System;

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
            log.Info("Services request.");
            string email = UserManager.GetAuthenticatedEmail();
            if (email != null && email.Length > 0) {
                return req.CreateResponse(HttpStatusCode.OK, "", "application/json");
            }

            StorageService service = new StorageService();
            string list = service.ListFilesOrDirectories(name.Replace("-", "/"));
            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.OK, list, "application/json");
        }

        [FunctionName("GetFile")]
        public static HttpResponseMessage GetFile(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", 
            Route = "GetFile/{path}/{file}")]HttpRequestMessage req, 
            string path, string file, TraceWriter log)
        {
            path = path.Replace("-", "/");
            string email = UserManager.GetAuthenticatedEmail();
            StorageService service = new StorageService();

            if (service.IsAllowDownload(email, path))
            {
                var content = service.GetContent(path, file);
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                content.DownloadToStream(stream, null);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                var result = new HttpResponseMessage(HttpStatusCode.OK);

                result.Content = new StreamContent(stream);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = file
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                return result;
            }
            else {
                return req.CreateResponse(HttpStatusCode.OK, "Access Denied");
            }
        }

        [FunctionName("Enrollment")]
        public static HttpResponseMessage GetEnrollment(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "Enrollment")]HttpRequestMessage req, TraceWriter log) {

            string email = UserManager.GetAuthenticatedEmail();
            if (email == null) {
                log.Warning("Services.GetEnrollment unauthenciated user.");
            }
            log.Info(email);

            StorageService service = new StorageService();
            string list = service.GetEnrollment(email);
            
            return req.CreateResponse(HttpStatusCode.OK, list, "application/json");
        }

        [FunctionName("GetUser")]
        public static HttpResponseMessage GetUser(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "User")]HttpRequestMessage req, TraceWriter log)
        {

            string user = UserManager.GetAuthenticatedUser();
            if (user == null)
            {
                log.Warning("Services.GetEnrollment unauthenciated user.");
            }
            log.Info(user);                         

            return req.CreateResponse(HttpStatusCode.OK, user);
        }

        [FunctionName("SignOut")]
        public static HttpResponseMessage SignOut(
          [HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "SignOut")]HttpRequestMessage req, TraceWriter log)
        {
            var cookies = req.Headers.GetCookies();
            var result = new HttpResponseMessage(HttpStatusCode.OK);

            //clean up auth cookie
            foreach (var cookieHeaderValue in cookies) {
                 foreach (var c in cookieHeaderValue.Cookies) {
                    log.Info(string.Format("Cookie name:{0}={1}",c.Name,c.Value));
                    var respCookie = new CookieHeaderValue(c.Name, string.Empty);
                    respCookie.Expires = DateTime.Now.AddDays(-1);
                    result.Headers.AddCookies(new CookieHeaderValue[] { respCookie });
                }
            }
            
            foreach (var h in req.Headers) {
                 log.Info(string.Format("header key {0}:{1}",h.Key,h.Value));
            }

            result.Content = new StringContent("Signed Out");
            return result;
        }
    }
}
