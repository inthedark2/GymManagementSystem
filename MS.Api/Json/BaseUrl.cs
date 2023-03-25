using System;
using System.Net.Http;

namespace MS.Api.Json
{
    public class BaseUrl
    {
        private readonly string _value;
        private readonly HttpRequestMessage _reqeustMessage;

        public BaseUrl(HttpRequestMessage reqeustMessage)
        {
            _reqeustMessage = reqeustMessage;
        }

        public BaseUrl(string val)
        {
            _value = val;
        }

        public override string ToString()
        {
            if (_reqeustMessage != null)
            {
                var baseUrl = _reqeustMessage.RequestUri.GetLeftPart(UriPartial.Authority);
                return baseUrl;
            }

            return _value;
        }
    }
}