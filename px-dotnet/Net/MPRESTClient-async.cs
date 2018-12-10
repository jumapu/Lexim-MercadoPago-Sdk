#if NETSTANDARD2_0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MercadoPago
{
    //Execute a request to an endpoint.
    //Api response with the result of the call.
    internal partial class MPRESTClient
    {
        public async Task<MPAPIResponse> ExecuteRequestAsync(
            HttpMethod httpMethod,
            string path,
            PayloadType payloadType,
            JObject payload,
            int requestTimeout,
            int retries)
        {

            System.Diagnostics.Trace.WriteLine("Payload " + httpMethod + " request to " + path + " : " + payload);

            try
            {
    
                if (string.IsNullOrEmpty(path))
                    throw new MPRESTException("Uri can not be an empty string.");

                ValidateMethodAndPayload(httpMethod, payload);

                var requestMethod = GetHttpMethod(httpMethod);
                var request = new HttpRequestMessage(requestMethod, path);

                var client = new HttpClient();

                if (requestTimeout > 0)
                {
                    client.Timeout = TimeSpan.FromSeconds(requestTimeout);
                }

                request.Headers.Add("HTTP.USER_AGENT", $"Lexim MercadoPago .NET SDK v{SDK.Version}");

                if (payload != null)
                {
                    string data;
                    if (payloadType != PayloadType.JSON)
                    {
                        var formParams = 
                            payload.ToObject<Dictionary<string, string>>()
                                   .Select(x => $"{x.Key}={x.Value}");

                        var parametersString =
                            string.Join("&", formParams);

                        data = parametersString;
                    }
                    else
                    {
                        data = payload.ToString();
                    }

                    var content = Encoding.ASCII.GetBytes(data);
                    request.Content = new ByteArrayContent(content);
                    request.Content.Headers.ContentLength = content.Length;
                    request.Content.Headers.ContentType =
                        payloadType == PayloadType.JSON
                            ? new MediaTypeHeaderValue("application/json")
                            : new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                }

                while (true)
                {
                    try
                    {
                        var response = await client.SendAsync(request);
                        var content = await response.Content.ReadAsByteArrayAsync();
                        return new MPAPIResponse(httpMethod, request, payload, response, content);
                    }
                    catch
                    {
                        if (--retries <= 0)
                            throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new MPRESTException(ex.Message, ex);
            }
        }

        private static System.Net.Http.HttpMethod GetHttpMethod(HttpMethod method)
        {
            switch (method)
            {
                case HttpMethod.GET:    return System.Net.Http.HttpMethod.Get;
                case HttpMethod.POST:   return System.Net.Http.HttpMethod.Post;
                case HttpMethod.PUT:    return System.Net.Http.HttpMethod.Put;
                case HttpMethod.DELETE: return System.Net.Http.HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, null);
            }
        }

        private static void ValidateMethodAndPayload(HttpMethod httpMethod, JObject payload)
        {
            switch (httpMethod)
            {
                case HttpMethod.GET when payload != null: throw new MPRESTException("Payload not supported for this method.");
                case HttpMethod.PUT when payload == null: throw new MPRESTException("Must include payload for this method.");
                case HttpMethod.DELETE when payload != null: throw new MPRESTException("Payload not supported for this method.");
            }
        }
    }
}

#endif