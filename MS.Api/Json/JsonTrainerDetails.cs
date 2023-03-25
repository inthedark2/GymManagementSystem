using MS.Api.Models;
using Newtonsoft.Json.Linq;

namespace MS.Api.Json
{
    public class JsonTrainerDetails
    {
        private readonly UserModel _userModel;

        public JsonTrainerDetails(UserModel userModel)
        {
            _userModel = userModel;
        }

        public JObject AsJson()
        {
            var jObject = new JObject();

            if (_userModel.Name != null) jObject["name"] = _userModel.Name;
            else jObject["name"] = "";

            if (_userModel.Surname != null) jObject["surname"] = _userModel.Surname;
            else jObject["surname"] = "";

            if (_userModel.PhoneNumber != null) jObject["phonenumber"] = _userModel.PhoneNumber;
            else jObject["phonenumber"] = "";

            if (_userModel.Email != null) jObject["email"] = _userModel.Email;
            else jObject["email"] = "";

            if (_userModel.Role != null)
            {
                if (_userModel.Role.Id != 0) jObject["rolename"] = _userModel.Role.RoleName;
                else jObject["rolename"] = "";
            }
            else
            {
                jObject["rolename"] = "";
            }

            if (_userModel.PhotoPath != null) jObject["phototrainer"] = _userModel.PhotoPath;
            else jObject["phototrainer"] = "";

            if (_userModel.Id != 0) jObject["trainerid"] = _userModel.Id;
            else jObject["trainerid"] = 0;

            return jObject;
        }
    }
}