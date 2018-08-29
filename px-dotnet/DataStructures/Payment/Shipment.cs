using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MercadoPago.DataStructures.Payment
{
    public struct Shipment
    {
        #region Properties 
        /// <summary>
        /// Buyer's address
        /// </summary>
        public ReceiverAddress? ReceiverAddress { get; set; }

        #endregion
    }
}
