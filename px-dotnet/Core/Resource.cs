using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using MercadoPago.Core.Linq;
using MercadoPago.Validation;
using Newtonsoft.Json.Linq;

namespace MercadoPago
{
    public abstract partial class Resource<T>: ResourceBase where T: ResourceBase, new()
    {
        // prevent derived classes outside this assembly.
        internal Resource()
        {
            
        }

        internal static MPAPIResponse Invoke(HttpMethod httpMethod, string path, PayloadType payloadType, JObject payload, string accessToken, Dictionary<string, string> queryParameters, bool useCache, int requestTimeout, int retries)
        {
            path = CreatePath(path, accessToken, queryParameters);

            var cacheKey = GetCacheKey(httpMethod, path);

            var response = TryGetFromCache(useCache, cacheKey);

            if (response == null)
            {
                response = new MPRESTClient().ExecuteRequest(
                    httpMethod,
                    path,
                    payloadType,
                    payload,
                    true,
                    requestTimeout,
                    retries);

                TryAddToCache(useCache, cacheKey, response);
            }

            return response;
        }

        internal static string CreatePath(string path, string accessToken, Dictionary<string, string> queryParameters)
        {
            if (string.IsNullOrEmpty(path))
                throw new MPException("Must specify a valid path.");

            var queryString =
                queryParameters != null
                    ? "&" + string.Join("&", queryParameters.Select(x => $"{x.Key}={x.Value}"))
                    : "";

            return $"{SDK.BaseUrl}{path}?access_token={accessToken ?? SDK.GetAccessToken()}{queryString}";
        }

        #region New approach

        internal static T Get(string path, string accessToken = null, bool useCache = false, int requestTimeout = 0, int retries = 1)
        {
            var resource = new T();
            var response = Invoke(HttpMethod.GET, path, PayloadType.NONE, null, accessToken, null, useCache, requestTimeout, retries);

            ProcessResponse(path, resource, response, HttpMethod.GET);
            return resource;
        }

        internal static List<T> GetList(string path, string accessToken = null, bool useCache = false, Dictionary<string, string> queryParameters = null, int requestTimeOut = 0, int retries = 1) 
        {
            var response = Invoke(HttpMethod.GET, path, PayloadType.NONE, null, accessToken, queryParameters, useCache, requestTimeOut, retries);

            if (response.StatusCode >= 200 && response.StatusCode < 300)
            {
                return response.ToList<T>();
            }

            var exception = new MPException
            {
                StatusCode = response.StatusCode,
                ErrorMessage = response.StringResponse
            };

            if (response.JsonObjectResponse != null)
                exception.Cause.Add(response.JsonObjectResponse.ToString());

            throw exception;
        }

        internal static IQueryable<T> CreateQuery(string path, string accessToken = null, bool useCache = false) =>
            new MpQueryable<T>(path, accessToken, useCache);

        internal T Post(string path, bool useCache = false, int requestTimeout = 0, int retries = 1) 
            => Send(this as T, HttpMethod.POST, path, useCache, requestTimeout, retries);

        internal T Put(string path, bool useCache = false, int requestTimeOut = 0, int retries = 1) 
            => Send(this as T, HttpMethod.PUT, path, useCache, requestTimeOut, retries);

        internal T Delete(string path, bool useCache = false, int requestTimeOut = 0, int retries = 1)
        {
            Send(this as T, HttpMethod.DELETE, path, useCache, requestTimeOut, retries);
            return null;
        }

        internal static T Send(T resource, HttpMethod httpMethod, string path, bool useCache = false, int requestTimeout = 0, int retries = 1)
        {
            var postOrPut = httpMethod == HttpMethod.POST || httpMethod == HttpMethod.PUT;

            var payload = GetPayload(resource, httpMethod);

            if (postOrPut)
                Validator.Validate(resource);

            var response = Invoke(httpMethod, path, PayloadType.JSON, payload, resource.UserAccessToken, null, useCache, requestTimeout, retries);

            ProcessResponse(path, resource, response, httpMethod);
            return resource;
        }

        internal static JObject GetPayload(T resource, HttpMethod httpMethod)
        {
            switch (httpMethod)
            {
                case HttpMethod.POST:
                    return resource.Serialize();
                case HttpMethod.PUT:
                    JObject jactual = resource.Serialize();
                    JObject jold = resource.LastKnownJson;
                    return Serialization.GetDiffFromLastChange(jactual, jold);
                default:
                    return null;
            }
        }

        internal static void ProcessResponse(string path, T resource, MPAPIResponse response, HttpMethod httpMethod)
        {
            var errorDetails = response.JsonObjectResponse?.ToString() ?? response.StringResponse ?? "[No additional details could be retrieved from the response]";
            var errorMessage =
                $"HTTP {httpMethod} request to Endpoint '{path}' with payload of type '{typeof(T).Name}' resulted in an unsuccessful HTTP response with status code {response.StatusCode}.\r\nDetails:\r\n{errorDetails}";

            if (response.StatusCode >= 200 && response.StatusCode < 300)
            {
                if (httpMethod != HttpMethod.DELETE)
                {
                    FillResourceWithResponseData(resource, response);
                }
            }
            else if (response.StatusCode >= 400 && response.StatusCode < 500)
            {
                var badParamsError = MPCoreUtils.GetBadParamsError(response.StringResponse);

                var exception = new MPException(errorMessage)
                {
                    Error = badParamsError,
                    ErrorMessage = badParamsError.ToString()
                };

                throw exception;
            }
            else
            {
                var exception = new MPException(errorMessage)
                {
                    StatusCode = response.StatusCode,
                    ErrorMessage = response.StringResponse,
                    Cause =
                    {
                        errorDetails
                    }
                };

                throw exception;
            }
        }

        internal static void FillResourceWithResponseData(T resource, MPAPIResponse response) 
        {
            if (response.JsonObjectResponse is JObject jsonObject)
            {
                var result = jsonObject.Deserialize<T>();
                CopyProperties(result, resource);
                resource.LastKnownJson = jsonObject;
                resource.LastApiResponse = response;
            }
        }

        private static void CopyProperties(T source, T destination)
        {
            var ignoreProperties =
                new[]
                {
                    nameof(LastKnownJson),
                    nameof(LastApiResponse),
                    "Errors",
                    nameof(UserAccessToken)
                };

            var properties =
                from p in destination.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                where !ignoreProperties.Contains(p.Name)
                let v = p.GetValue(source, null)
                select new
                {
                    Property = p,
                    Value = v
                };

            foreach (var p in properties)
                p.Property.SetValue(destination, p.Value,null);
        }

        private static void TryAddToCache(bool useCache, string cacheKey, MPAPIResponse response)
        {
            if (useCache)
            {
                MPCache.AddToCache(cacheKey, response);
            }
            else
            {
                MPCache.RemoveFromCache(cacheKey);
            }
        }

        private static string GetCacheKey(HttpMethod httpMethod, string path) => $"{httpMethod}_{path}";

        private static MPAPIResponse TryGetFromCache(bool useCache, string cacheKey)
        {
            MPAPIResponse response = null;
            if (useCache)
            {
                response = MPCache.GetFromCache(cacheKey);

                if (response != null)
                {
                    response.IsFromCache = true;
                }
            }

            return response;
        }

        #endregion
    }
}