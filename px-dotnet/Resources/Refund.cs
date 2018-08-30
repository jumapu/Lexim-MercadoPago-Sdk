using System;
namespace MercadoPago.Resources
{
    public sealed class Refund : Resource<Refund>
    {
        #region Actions
        
        public Refund Save() => Post($"/v1/payments/{PaymentId}/refunds");

        #endregion

        #region Properties

        public decimal? Id { get; private set; }

        public decimal? Amount { get; set; }

        public decimal? PaymentId { get; internal set; }

        public DateTime? DateCreated { get; private set; }

        public new string Errors { get; set; }

        #endregion


    }
}
