using System.Diagnostics;
using MercadoPago;
using MercadoPago.Common;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;

namespace MercadoPagoSDK.Samples
{
    internal class UserAccessToken2 : ISample
    {
        public string Name => "Per-Request Access Token #2";

        public string Category => "Checkout";

        public void Run()
        {
            /*
           // --------------------------------------- \\
           || Example #2: Using ClientId/ClientSecret ||
           \\ --------------------------------------- //
           */

            // if you have a merchant-specific ClientId/ClientSecret, you need to call this method first to obtain the AccessToken:
            var clientId = Utils.Prompt("Enter Client Id: ");
            var clientSecret = Utils.Prompt("Enter Client Secret: ");

            var accessToken = SDK.GetAccessToken(clientId, clientSecret);

            var preference = new Preference
            {
                // Then use the Access Token obtained above
                UserAccessToken = accessToken,
                Items =
                {
                    new Item
                    {
                        Id = "1234",
                        Title = "Small Silk Plate",
                        Quantity = 5,
                        CurrencyId = CurrencyId.ARS,
                        UnitPrice = 44.23m
                    }
                },
                Payer = new Payer
                {
                    Email = "augustus_mckenzie@gmail.com"
                }
            };

            // Save and posting preference
            preference.Save();

            Process.Start(preference.InitPoint);
        }
    }
}