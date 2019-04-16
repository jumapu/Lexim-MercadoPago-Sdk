using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MercadoPago.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PreapprovalStatus
    {
        ///<summary>The preapproval needs to be authorized by the user</summary>
        pending,
        ///<summary>The preapproval is authorized to debit automatically from the user credit card</summary>
        authorized,
        ///<summary>No charges will be made during this state (stop debit)</summary>
        paused,
        ///<summary>The authorization is no longer active</summary>
        cancelled
    }

}
