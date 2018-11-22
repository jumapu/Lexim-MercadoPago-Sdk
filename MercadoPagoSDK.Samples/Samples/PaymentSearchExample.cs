using MercadoPago.Resources;
using System.Linq;
using MercadoPago.Common;

namespace MercadoPagoSDK.Samples
{
    internal class PaymentSearchExample: ISample, IRequiresAccessToken
    {
        public string Name => "Search Payments using a LINQ Query";

        public string Category => "Payments";

        public void Run()
        {
            Utils.LoadOrPromptAccessToken();

            var allPayments = Payment.All();

            var approvedPayments = 
                Payment.Query()
                       .Where(x => x.Status == PaymentStatus.approved)
                       .ToList();

            var rejectedPayments = 
                Payment.Query()
                       .Where(x => x.Status == PaymentStatus.rejected)
                       .ToList();
        }
    }
}