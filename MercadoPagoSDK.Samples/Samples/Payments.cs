using MercadoPago.DataStructures.Payment;
using MercadoPago.Resources;

namespace MercadoPagoSDK.Samples
{
    internal class Payments : ISample, IRequiresAccessToken
    {
        public string Name => "Payments with API";

        public string Category => "Payments";

        public void Run()
        {
            var payment = new Payment
            {
                TransactionAmount = 1000,
                Token = "card_token_id",
                Description = "Cats",
                ExternalReference = "YOUR_REFERENCE",
                PaymentMethodId = "visa",
                Installments = 1,
                Payer = new Payer
                {
                    Email = "some@mail.com",
                    FirstName = "Diana",
                    LastName = "Prince"
                },
            };

            payment.Save();
        }
    }
}
