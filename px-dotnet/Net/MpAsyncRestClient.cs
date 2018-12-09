using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

#if NETSTANDARD2_0

namespace MercadoPago
{
    internal class MpAsyncRestClient
    {
        /// <summary>
        /// class to simulate HttpClient class, available from .NET 4.0 onward.
        /// </summary>
        private class MPRequest
        {

            public HttpWebRequest Request { get; set; }
            public byte[] RequestPayload { get; set; }

        }


        public string ProxyHostName = null;
        public int ProxyPort = -1;

        static MpAsyncRestClient()
        {
            ServicePointManager.SecurityProtocol |= (SecurityProtocolType)3072;

            var v1 = Convert.ToUInt64((SecurityProtocolType)3072);
            var v2 = Convert.ToUInt64(ServicePointManager.SecurityProtocol);

            var hasFlag = (v2 & v1) == v1;

            if (!hasFlag)
                throw new NotSupportedException($"MercadoPago API requires TLS 1.2, which is not enabled on this machine. Please refer to https://www.mercadopago.com.ar/developers/es/guides/pci-compliant-merchants/disabling-tls-10 for details on how to fix this error.");
        }

        #region Core Methods
        /// <summary>
        /// Class constructor.
        /// </summary>
        public MpAsyncRestClient()
        {
            new MPRESTClient(null, -1);
        }

        /// <summary>
        /// Set class variables.
        /// </summary>
        /// <param name="proxyHostName">Proxy host to use.</param>
        /// <param name="proxyPort">Proxy port to use in the proxy host.</param>
        public MpAsyncRestClient(string proxyHostName, int proxyPort)
        {
            this.ProxyHostName = proxyHostName;
            this.ProxyPort = proxyPort;
        }

        /// <summary>
        /// Execute a request to an endpoint.
        /// </summary>
        /// <param name="httpMethod">Method to use in the request.</param>
        /// <param name="path">Endpoint we are pointing.</param>
        /// <param name="payloadType">Type of payload we are sending along with the request.</param>
        /// <param name="payload">The data we are sending.</param>
        /// <param name="colHeaders">Extra headers to send with the request.</param>
        /// <returns>Api response with the result of the call.</returns>
        public MPAPIResponse ExecuteRequest(
            HttpMethod httpMethod,
            string path,
            PayloadType payloadType,
            JObject payload,
            bool includeHeaders,
            int requestTimeout,
            int retries)
        {

            System.Diagnostics.Trace.WriteLine("Payload " + httpMethod + " request to " + path + " : " + payload);

            var headers =
                includeHeaders
                    ? new Dictionary<string, string>
                    {
                        ["HTTP.CONTENT_TYPE"] = "application/json",
                        ["HTTP.USER_AGENT"] = "MercadoPago .NET SDK v1.0.1"
                    }
                    : null;

            try
            {
    
                if (string.IsNullOrEmpty(path))
                    throw new MPRESTException("Uri can not be an empty string.");

                ValidateMethodAndPayload(httpMethod, payload);

                var requestMethod = GetHttpMethod(httpMethod);
                var request = new HttpRequestMessage(requestMethod, path;

                var client = new HttpClient();

                if (requestTimeout > 0)
                {
                    client.Timeout = TimeSpan.FromSeconds(requestTimeout);
                }

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }
                }

                request.Headers.

                mpRequest.Request.ContentType = "application/json";
                mpRequest.Request.UserAgent = "MercadoPago DotNet SDK/1.0.30";

                if (payload != null) // POST & PUT
                {
                    byte[] data = null;
                    if (payloadType != PayloadType.JSON)
                    {
                        var parametersDict = payload.ToObject<Dictionary<string, string>>();
                        StringBuilder parametersString = new StringBuilder();
                        parametersString.Append(string.Format("{0}={1}", parametersDict.First().Key, parametersDict.First().Value));
                        parametersDict.Remove(parametersDict.First().Key);
                        foreach (var value in parametersDict)
                        {
                            parametersString.Append(string.Format("&{0}={1}", value.Key, value.Value.ToString()));
                        }

                        data = Encoding.ASCII.GetBytes(parametersString.ToString());
                    }
                    else
                    {
                        data = Encoding.ASCII.GetBytes(payload.ToString());
                    }

                    mpRequest.Request.ContentLength = data.Length;
                    mpRequest.Request.ContentType = payloadType == PayloadType.JSON ? "application/json" : "application/x-www-form-urlencoded";
                    mpRequest.RequestPayload = data;
                }

                if ((httpMethod == HttpMethod.POST || httpMethod == HttpMethod.PUT) && mpRequest.RequestPayload != null)
                {
                    Stream requestStream = mpRequest.Request.GetRequestStream();
                    requestStream.Write(mpRequest.RequestPayload, 0, mpRequest.RequestPayload.Length);
                    requestStream.Close();
                }

                try
                {
                    
                   
                   

                    using (HttpWebResponse response = (HttpWebResponse)mpRequest.Request.GetResponse())
                    {

                        return new MPAPIResponse(httpMethod, mpRequest.Request, payload, response);
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                        return new MPAPIResponse(httpMethod, mpRequest.Request, payload, errorResponse);
                    }
                    else
                    {
                        if (--retries == 0)
                            throw;
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new MPRESTException(ex.Message);
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
            if (httpMethod.Equals(HttpMethod.GET))
            {
                if (payload != null)
                {
                    throw new MPRESTException("Payload not supported for this method.");
                }
            }
            else if (httpMethod.Equals(HttpMethod.POST))
            {
                //if (payload == null)
                //{
                //    throw new MPRESTException("Must include payload for this method.");
                //}
            }
            else if (httpMethod.Equals(HttpMethod.PUT))
            {
                if (payload == null)
                {
                    throw new MPRESTException("Must include payload for this method.");
                }
            }
            else if (httpMethod.Equals(HttpMethod.DELETE))
            {
                if (payload != null)
                {
                    throw new MPRESTException("Payload not supported for this method.");
                }
            }
        }

        #endregion
    }
}

#endif