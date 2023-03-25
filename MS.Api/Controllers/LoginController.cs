using System.Net.Http;
using System.Web.Http;
using MS.Api.Models;
using MS.BusinessLayer.Services;
using MS.DataLayer.Concrete;
using MS.DataLayer.Entities;
using Newtonsoft.Json.Linq;

namespace MS.Api.Controllers
{
    public class LoginController : ApiController
    {
        private readonly UserService _userService;

        public LoginController()
        {
            _userService = new UserService(new UserRepository(new ManagmentSystemContext()));
        }

        [HttpPost, Route("api/auth/login")]
        public JObject Login(HttpRequestMessage request)
        {
            var response = new ResponseModel();

            var jsonString = request?.Content.ReadAsStringAsync().Result;
            var user = JObject.Parse(jsonString);

            var login = user["login"].Value<string>();
            var pass = user["password"].Value<string>();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
                return new JObject
                {
                    ["response"] = "Can't find manager. Incorrect login or password."
                };

            if (!_userService.CheckManagerPassword(login, pass))
                return new JObject
                {
                    ["response"] = "Can't find manager. Incorrect login or password."
                };

            response.Result = _userService.CheckManagerPassword(login, pass);
            var resultJson = new JObject { ["result"] = response.Result };
            return resultJson;
        }

        [HttpGet]
        [Route("api/testapi")]
        public string TestApi()
        {
            return "Success get api request. Yoy.";
        }
    }
}
