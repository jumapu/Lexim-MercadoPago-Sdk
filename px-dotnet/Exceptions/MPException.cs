using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using MercadoPago.DataStructures.Generic;

namespace MercadoPago
{
    [Serializable]
    public class MPException : Exception
    {
        #region Variables
        public string RequestId { get; }
		public int? StatusCode { get; set; } 
        public string ErrorMessage { get; internal set; } 
        
        public List<string> Cause { get; internal set; } = new List<string>();
        public BadParamsError Error { get; internal set; }
        #endregion

        #region Constructors

        public MPException(SerializationInfo info, StreamingContext context) : base( info, context ){ 
            this.ErrorMessage = info.GetString("ErrorMessage");
        }


        public MPException(string message) : base(message) {
            
		}

		public MPException(string message, Exception innerException) : base( message, innerException ) {
		}

		public MPException(string message, string requestId, int? statusCode) : base( message ) {
			RequestId = requestId;
			StatusCode = statusCode;
		} 

		public MPException(String message, String requestId, int statusCode, Exception innerException) 
					: base( message, innerException ){
			RequestId = requestId;
			StatusCode = statusCode;
		}

        public MPException(int statusCode, string message, List<string> cause): base(message)
        {
            StatusCode = statusCode; 
            Cause = cause;
        }

        public MPException() 
        {
            
        }

        #endregion

        #region Class Utils
        /// <summary>
        /// Format a exception error.
        /// </summary>
        /// <returns>Formated exception.</returns>
        public override string ToString() {
			string reqIdStr = string.Empty;
			if (!string.IsNullOrEmpty(RequestId))
				reqIdStr = "; request-id: " + RequestId;

            string statCodeStr = string.Empty;
			if (StatusCode.HasValue)
				statCodeStr = "; status_code: " + StatusCode.Value;

			return base.ToString() + reqIdStr + statCodeStr;
		}
        #endregion

        #region ISerializable Members

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("RequestId", RequestId);
            info.AddValue("StatusCode", StatusCode);
        }

        #endregion
    }
}
