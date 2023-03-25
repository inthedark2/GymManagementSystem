using System;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace MS.Api.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet, Route("")]
        public JObject Get(HttpRequestMessage req)
        {
            var response = new JObject();
            var links = new JObject();
            var baseUrl = new Url(req.ToString());

            links.Add("status", baseUrl + "/status");

            response["links"] = links;
            response["utc"] = DateTime.UtcNow;

            return response;
        }
    }
}
