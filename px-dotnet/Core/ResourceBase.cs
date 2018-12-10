using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MercadoPago
{
    public abstract partial class ResourceBase
    {
        // prevent derived classes outside this assembly.
        internal ResourceBase()
        {
            
        }

        internal MPAPIResponse LastApiResponse { get; set; }

        internal JObject LastKnownJson { get; set; }

        /// <summary>
        /// Can be used to provide per-instance or per-request Access Tokens for the MercadoPago API. 
        /// If left null/empty, then the SDK.AccessToken is used instead.
        /// </summary>
        [JsonIgnore]
        public string UserAccessToken { get; set; }

        internal JObject GetJsonSource() => LastApiResponse?.JsonObjectResponse;
    }
}