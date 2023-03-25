using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MS.Api.Models;
using Newtonsoft.Json.Linq;

namespace MS.Api.Json
{
    internal class JsonClients
    {
        private readonly ClientJson _client;

        public JsonClients(ClientJson client)
        {
            _client = client;
        }

        public JObject AsJson()
        {
            JObject x = new JObject
            {
                ["name"] = _client.Name,
                ["surname"] = _client.Surname,
                ["secondname"] = _client.SecondName,
                ["phonenumber"] = _client.PhoneNumber,
                ["email"] = _client.Email,
                ["id"] = _client.Id
            };
            return x;
        }
    }
}