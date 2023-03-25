using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MS.Api.Json;
using MS.Api.Models;
using MS.BusinessLayer.IServices;
using MS.BusinessLayer.Services;
using MS.DataLayer.Concrete;
using MS.DataLayer.Entities;
using Newtonsoft.Json.Linq;
using MS.Common.Enums;

namespace MS.Api.Controllers
{
    public class TrainerController : ApiController
    {
        private readonly IUserService _userService;

        public TrainerController()
        {
            _userService = new UserService(new UserRepository(new ManagmentSystemContext()));
        }

        [HttpGet]
        [Route("api/alltrainers")]
        public JObject All(HttpRequestMessage request)
        {
            return AllTrainers(new BaseUrl(request));
        }

        [HttpGet]
        [Route("api/gettrainer")]
        public JObject GetTrainerByEmail(HttpRequestMessage request, string trainerEmail)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<Role, RoleModel>();
            });

            var trainerModel = Mapper.Map<User, UserModel>(_userService.GetTrainerByEmail(trainerEmail));

            if (trainerModel != null)
            {
                return new JsonTrainerDetails(trainerModel).AsJson();
            }

            return new JObject
            {
                ["response"] = "Trainer not found."
            };
        }

        private JObject AllTrainers(BaseUrl baseUrl)
        {
            var allTrainers = _userService.GetAllTrainers();

            var responce = new JObject();
            var trainersArray = new JArray();

            foreach (var leadTrainer in allTrainers)
            {
                var jObject = new JObject
                {
                    ["name"] = leadTrainer.Name,
                    ["surname"] = leadTrainer.Surname,
                    ["email"] = leadTrainer.Email,
                    ["photopath"] = leadTrainer.PhotoPath,
                    ["self"] = baseUrl + "/trainers/" + leadTrainer.Id
                };
                trainersArray.Add(jObject);
            }

            responce["trainers"] = trainersArray;
            return responce;
        }

        [HttpPost, Route("api/edittrainer")]
        public JObject EditTrainer(HttpRequestMessage request)
        {
            var jsonString = request?.Content.ReadAsStringAsync().Result;
            var trainer = JObject.Parse(jsonString);

            var name = trainer["name"].Value<string>();
            var surname = trainer["surname"].Value<string>();
            var phonenumber = trainer["phonenumber"].Value<string>();
            var email = trainer["email"].Value<string>();

            if (_userService.UpdateTrainer(name, surname, phonenumber, email) == false)
                return new JObject
                {
                    ["response"] = "Not found trainer in db."
                };

            return new JObject
            {
                ["response"] = "Trainer successfull edit."
            };
        }

        [HttpPost, Route("api/deletetrainer")]
        public JObject DeleteTrainer(HttpRequestMessage request)
        {
            var jsonString = request?.Content.ReadAsStringAsync().Result;
            var client = JObject.Parse(jsonString);

            var email = client["email"].Value<string>();

            var trainerDb = _userService.GetTrainerByEmail(email);

            if (trainerDb != null)
            {
                _userService.RemoveTrainer(trainerDb.Id);

                return new JObject
                {
                    ["response"] = "Trainer successfull deleted."
                };
            }

            return new JObject
            {
                ["response"] = "Not found current trainer in db."
            };
        }

        [HttpPost, Route("api/addtrainer")]
        public JObject AddTrainer(HttpRequestMessage request)
        {
            var jsonString = request?.Content.ReadAsStringAsync().Result;
            var trainer = JObject.Parse(jsonString);

            var name = trainer["name"].Value<string>();
            var surname = trainer["surname"].Value<string>();
            var phonenumber = trainer["phonenumber"].Value<string>();
            var email = trainer["email"].Value<string>();

            var rolename = EUserTypes.Trainer.ToString();

            var trainerDb = _userService.AddTrainer(name, surname, phonenumber, email, rolename);

            return trainerDb != null ? new JObject { ["response"] = "Trainer add." } : new JObject { ["response"] = "Trainer not add in db." };
        }
    }
}
