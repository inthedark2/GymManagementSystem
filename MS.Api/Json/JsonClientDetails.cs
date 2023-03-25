using MS.Api.Models;
using Newtonsoft.Json.Linq;

namespace MS.Api.Json
{
    internal class JsonClientDetails
    {
        private readonly ClientModel _clientModel;

        public JsonClientDetails(ClientModel client)
        {
            _clientModel = client;
        }

        public JObject AsJson()
        {
            var jObject = new JObject();

            if (_clientModel.Name != null) jObject["name"] = _clientModel.Name;
            else jObject["name"] = "";

            if (_clientModel.Surname != null) jObject["surname"] = _clientModel.Surname;
            else jObject["surname"] = "";

            if (_clientModel.SecondName != null) jObject["secondname"] = _clientModel.SecondName;
            else jObject["secondname"] = "";

            if (_clientModel.PhoneNumber != null) jObject["phonenumber"] = _clientModel.PhoneNumber;
            else jObject["phonenumber"] = "";

            if (_clientModel.Email != null) jObject["email"] = _clientModel.Email;
            else jObject["email"] = "";

            if (_clientModel.ClubCardId != null)
            {
                if (_clientModel.ClubCardId.Id != 0) jObject["clubcardid"] = _clientModel.ClubCardId.Id;
                else jObject["clubcardid"] = 0;

                if (_clientModel.ClubCardId.ClubCardNumber != null) jObject["clubcardnumber"] = _clientModel.ClubCardId.ClubCardNumber;
                else jObject["clubcardnumber"] = "";
            }
            else
            {
                jObject["clubcardid"] = 0;
                jObject["clubcardnumber"] = "";
            }

            if (_clientModel.GenderId != null)
            {
                if (_clientModel.GenderId.Name != null) jObject["gender"] = _clientModel.GenderId.Name;
                else jObject["gender"] = "";
            }
            else
                jObject["gender"] = "";

            if (_clientModel.Id != 0) jObject["clientid"] = _clientModel.Id;
            else jObject["clientid"] = 0;

            if (_clientModel.PhotoPath != null) jObject["photoclient"] = _clientModel.PhotoPath;
            else jObject["photoclient"] = "";

            return jObject;
        }
    }
}