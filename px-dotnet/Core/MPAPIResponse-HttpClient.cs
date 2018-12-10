#if NETSTANDARD2_0

using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MercadoPago
{
    internal partial class MPAPIResponse
    {
        public MPAPIResponse(HttpMethod httpMethod, HttpRequestMessage request, JObject payload, HttpResponseMessage response, byte[] responseContent)
        {
            HttpMethod = httpMethod.ToString();
            Url = request.RequestUri.ToString();
            if (payload != null)
            {
                Payload = payload.ToString();
            }

            StatusCode = (int)response.StatusCode;
            StatusDescription = response.ReasonPhrase;

            StringResponse = Encoding.UTF8.GetString(responseContent);

            try
            {
                JsonObjectResponse = JObject.Parse(StringResponse);
            }
            catch
            {
                // ignored
            }
        }
    }
}

#endif