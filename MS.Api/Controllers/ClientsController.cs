using System.Linq;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MS.Api.Json;
using MS.Api.Models;
using MS.BusinessLayer.Services;
using MS.DataLayer.Concrete;
using MS.DataLayer.Entities;
using Newtonsoft.Json.Linq;

namespace MS.Api.Controllers
{
    public class ClientsController : ApiController
    {
        private readonly ClientService _clientService;

        public ClientsController()
        {
            _clientService = new ClientService(new ClientRepository(new ManagmentSystemContext()));
        }

        [HttpGet]
        [Route("allclients")]
        public JObject All(HttpRequestMessage request)
        {
            return AllClients(new BaseUrl(request));
        }

        [HttpGet]
        [Route("getclient")]
        public JObject GetClientByEmail(HttpRequestMessage request, string clientEmail)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Client, ClientJson>());

            ClientJson clientJson = Mapper.Map<Client, ClientJson>(_clientService.GetClientByEmail(clientEmail));

            return new JsonClients(clientJson).AsJson();
        }

        private JObject AllClients(BaseUrl baseUrl)
        {
            var allClients = _clientService.Clients().ToList();

            var responce = new JObject();
            var clientsArray = new JArray();

            foreach (var leadClient in allClients)
            {
                var jObject = new JObject
                {
                    ["name"] = leadClient.Name,
                    ["surname"] = leadClient.Surname,
                    ["email"] = leadClient.Email,
                    ["self"] = baseUrl + "/clients/" + leadClient.Id
                };
                clientsArray.Add(jObject);
            }
            responce["clients"] = clientsArray;
            return responce;
        }
    }
}
