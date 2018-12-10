using System;
namespace MercadoPago.Resources
{
    public sealed partial class Refund : Resource<Refund>
    {
        #region Actions
        
        public Refund Save() => Post($"/v1/payments/{PaymentId}/refunds");

        #endregion

        #region Properties

        public decimal? Id { get; private set; }

        public decimal? Amount { get; set; }

        public long? PaymentId { get; internal set; }

        public DateTime? DateCreated { get; private set; }

        public string Errors { get; set; }

        #endregion


    }
}
