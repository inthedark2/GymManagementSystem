using System.Net.Http;
using System.Web.Http;
using MS.Api.Json;
using MS.BusinessLayer.Services;
using MS.Common.Enums;
using MS.DataLayer.Concrete;
using MS.DataLayer.Entities;
using Newtonsoft.Json.Linq;

namespace MS.Api.Controllers
{
    public class TrainingController : ApiController
    {
        private readonly TrainingService _trainingService;

        public TrainingController()
        {
            _trainingService = new TrainingService(new TrainingRepository(new ManagmentSystemContext()));
        }

        [HttpGet]
        [Route("api/alltrainingrecords")]
        public JObject All(HttpRequestMessage request)
        {
            return AllTrainingRecords(new BaseUrl(request));
        }

        private JObject AllTrainingRecords(BaseUrl baseUrl)
        {
            var allRecords = _trainingService.GetAllRecords();

            var responce = new JObject();
            var recordsArray = new JArray();


            foreach (var leadRecord in allRecords)
            {
                var jObject = new JObject
                {

                    ["statusid"] = ((ETrainingRecordStatus)leadRecord.StatusId).ToString(),
                    ["trainingdate"] = leadRecord.TrainingDate.ToString("yyyy-MM-dd HH\':\'mm\':\'ss"),
                    ["trainername"] = leadRecord.Trainer.Name + " " + leadRecord.Trainer.Surname,
                    ["trainerphoto"] = leadRecord.Trainer.PhotoPath,
                    ["self"] = baseUrl + "/training records/" + leadRecord.Id
                };
                recordsArray.Add(jObject);
            }
            responce["training records"] = recordsArray;
            return responce;
        }
    }
}
