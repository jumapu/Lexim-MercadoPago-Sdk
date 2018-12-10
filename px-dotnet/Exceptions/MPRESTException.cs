using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MercadoPago
{
    [Serializable]
    public class MPRESTException : MPException
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message.</param>
        internal MPRESTException(string message) : base(message)
        {
        }

        internal MPRESTException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #endregion
    }
}
