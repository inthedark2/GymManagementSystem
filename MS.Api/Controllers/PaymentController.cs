using System.Net.Http;
using System.Web.Http;
using MS.Api.Json;
using MS.BusinessLayer.IServices;
using MS.BusinessLayer.Services;
using MS.Common.Enums;
using MS.DataLayer.Concrete;
using MS.DataLayer.Entities;
using MS.Localization;
using Newtonsoft.Json.Linq;

namespace MS.Api.Controllers
{
    public class PaymentController : ApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController()
        {
            _paymentService = new PaymentService(new PaymentRepository(new ManagmentSystemContext()));
        }

        [HttpGet]
        [Route("api/allpayments")]
        public JObject All(HttpRequestMessage request)
        {
            return AllPayments(new BaseUrl(request));
        }

        private JObject AllPayments(BaseUrl baseUrl)
        {
            var allPayments = _paymentService.GetAllPayments();

            var responce = new JObject();
            var peymentsArray = new JArray();

            foreach (var leadPayment in allPayments)
            {
                var jObject = new JObject
                {
                     ["paymentdate"] = leadPayment.PaymentDate.ToString("yyyy-MM-dd HH\':\'mm\':\'ss"),
                };

                if (leadPayment.Client != null)
                {
                    jObject["fullnamepaymer"] = leadPayment.Client.Name + " " + leadPayment.Client.Surname;
                }

                if (leadPayment.Subscription != null)
                {
                    jObject["paymentdescription"] = ((ESubscriptionType)leadPayment.Subscription.SubscriptionType.Id).GetLocalizedValue();
                    jObject["paymentprice"] = leadPayment.Subscription.SubscriptionType.Price;
                    jObject["paymentstatus"] = ((EPaymentStatus)leadPayment.PaymentStatusId).GetLocalizedValue();
                }

                jObject["self"] = baseUrl + "/peyments/" + leadPayment.Id;
                peymentsArray.Add(jObject);
            }

            responce["peyments"] = peymentsArray;
            return responce;
        }
    }
}
