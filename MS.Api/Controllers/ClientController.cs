using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MS.Api.Json;
using MS.Api.Models;
using MS.BusinessLayer.Services;
using MS.Common.Enums;
using MS.DataLayer.Concrete;
using MS.DataLayer.Entities;
using Newtonsoft.Json.Linq;

namespace MS.Api.Controllers
{
    public class ClientController : ApiController
    {
        private readonly ClientService _clientService;
        private readonly SubscriptionService _subscriptionService;
        private readonly CardService _cardService;

        public ClientController()
        {
            _subscriptionService = new SubscriptionService(new SubscriptionRepository(new ManagmentSystemContext()));
            _clientService = new ClientService(
                new ClientRepository(new ManagmentSystemContext()));
        }

        [HttpGet]
        [Route("api/allclients")]
        public JObject All(HttpRequestMessage request)
        {
            return AllClients(new BaseUrl(request));
        }

        [HttpGet]
        [Route("api/getclient")]
        public JObject GetClientByEmail(HttpRequestMessage request, string clientEmail)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Client, ClientModel>();
                cfg.CreateMap<ClubCard, ClubCardModel>();
                cfg.CreateMap<Gender, GenderModel>();
            });

            var clientModel = Mapper.Map<Client, ClientModel>(_clientService.GetClientByEmail(clientEmail));

            if (clientModel != null)
            {
                return new JsonClientDetails(clientModel).AsJson();
            }

            return new JObject
            {
                ["response"] = "Client not found."
            };
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
                    ["photopath"] = leadClient.PhotoPath,
                    ["self"] = baseUrl + "/clients/" + leadClient.Id
                };
                clientsArray.Add(jObject);
            }
            responce["clients"] = clientsArray;
            return responce;
        }

        [HttpPost, Route("api/editclient")]
        public JObject EditClient(HttpRequestMessage request)
        {
            var jsonString = request?.Content.ReadAsStringAsync().Result;
            var client = JObject.Parse(jsonString);

            var name = client["name"].Value<string>();
            var surname = client["surname"].Value<string>();
            var secondname = client["secondname"].Value<string>();
            var phonenumber = client["phonenumber"].Value<string>();
            var email = client["email"].Value<string>();
            var clubcardnumber = client["clubcardnumber"].Value<string>();

            if (_clientService.UpdateClient(name, surname, secondname, phonenumber, email, clubcardnumber) == false)
                return new JObject
                {
                    ["response"] = "Not found client in db."
                };

            return new JObject
            {
                ["response"] = "Client successfull edit."
            };
        }

        [HttpPost, Route("api/addclient")]
        public JObject AddClient(HttpRequestMessage request)
        {
            var jsonString = request?.Content.ReadAsStringAsync().Result;
            var client = JObject.Parse(jsonString);

            var name = client["name"].Value<string>();
            var surname = client["surname"].Value<string>();
            var secondname = client["secondname"].Value<string>();
            var phonenumber = client["phonenumber"].Value<string>();
            var email = client["email"].Value<string>();
            var gender = client["gender"].Value<int>();

            var clientDb = _clientService.AddClient(name, surname, secondname, phonenumber, email, (EGenderType)gender);

            return clientDb != null ? new JObject { ["response"] = "Client add." } : new JObject { ["response"] = "Client not add in db." };
        }

        [HttpPost, Route("api/addsubscriptionforclient")]
        public JObject AddSubscriptionForClient(HttpRequestMessage request)
        {
            var jsonString = request?.Content.ReadAsStringAsync().Result;
            var subscriptionTypeJson = JObject.Parse(jsonString);

            var subscriptionType = subscriptionTypeJson["subscriptionType"].Value<string>();
            var clientEmail = subscriptionTypeJson["clientemail"].Value<string>();
            var activateSubscription = subscriptionTypeJson["activatesubscription"].Value<bool>();

            var clientDb = _clientService.GetClientByEmail(clientEmail);
            var countSubs = _subscriptionService.GetSubscriptions().ToList().Count;

            SubscriptionType clientSubscriptionType = _subscriptionService.GetSubscriptionTypeByName(subscriptionType);
            var newSubs =_subscriptionService.AddNewSubscription(clientDb.ClubCardId.Id, clientSubscriptionType.Id, activateSubscription==true? ESubscriptionStatus.Active: ESubscriptionStatus.Payed);

            if(activateSubscription == true)
            {
                _subscriptionService.StartSubscriptionForApiByCardId(newSubs.Id);

                var countSubsNew = _subscriptionService.GetSubscriptions().ToList().Count;

                return countSubsNew > countSubs ? new JObject { ["response"] = "Subscription added and activated." } : new JObject { ["response"] = "Subscription not add in client." };
            }

            var countSubsNewSecond = _subscriptionService.GetSubscriptions().ToList().Count;

            return countSubsNewSecond > countSubs ? new JObject { ["response"] = "Subscription added." } : new JObject { ["response"] = "Subscription not add in client." };
        }

        [HttpGet]
        [Route("api/getsubscriptiontypes")]
        public JArray GetClientSubscriptionTypes(HttpRequestMessage request)
        {
            var subscriptionTypes = _subscriptionService.GetSubscriptionTypes();

            if (subscriptionTypes == null)
                return new JArray
                {
                    ["response"] = "Subscription types not found in db."
                };

            var array = JArray.FromObject(subscriptionTypes);
            return array;
        }
    }
}
