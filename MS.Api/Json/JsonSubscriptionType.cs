using MS.Api.Models;
using Newtonsoft.Json.Linq;

namespace MS.Api.Json
{
    public class JsonSubscriptionType
    {
        private readonly SubscriptionTypeModel _subscriptionTypeModel;

        public JsonSubscriptionType(SubscriptionTypeModel subscriptionTypeModel)
        {
            _subscriptionTypeModel = subscriptionTypeModel;
        }

        public JObject AsJson()
        {
            var jObject = new JObject();

            if (_subscriptionTypeModel.Name != null) jObject["subscriptiontype"] = _subscriptionTypeModel.Name;
            else jObject["subscriptiontype"] = "";

            if (_subscriptionTypeModel.Description != null) jObject["description"] = _subscriptionTypeModel.Description;
            else jObject["description"] = "";

            if (_subscriptionTypeModel.Price != 0) jObject["price"] = _subscriptionTypeModel.Price;
            else jObject["price"] = 0;

            if (_subscriptionTypeModel.MaxFrozenDays != 0) jObject["maxfrozendays"] = _subscriptionTypeModel.MaxFrozenDays;
            else jObject["maxfrozendays"] = 0;

            if (_subscriptionTypeModel.Id != 0) jObject["subscriptiontypeid"] = _subscriptionTypeModel.Id;
            else jObject["subscriptionTypeId"] = 0;
         
            return jObject;
        }
    }
}