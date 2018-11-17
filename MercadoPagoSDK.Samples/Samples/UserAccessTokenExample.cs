using System.Diagnostics;
using MercadoPago;
using MercadoPago.Common;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;

namespace MercadoPagoSDK.Samples
{
    internal class UserAccessTokenExample: ISample
    {
        public string Name => "Per-Request Access Token";

        public string Category => "Checkout";

        public void Run()
        {
            /*
            // --------------------------------------- \\
            || Example #1: Using AccessToken           ||
            \\ --------------------------------------- //
            */

            // Create a preference object
            var preference = new Preference
            {
                // If you want to use a merchant-specific Access Token for this particular request, you can use this property.
                UserAccessToken = "YOUR_ACCESS_TOKEN",
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

            /*
            // --------------------------------------- \\
            || Example #2: Using ClientId/ClientSecret ||
            \\ --------------------------------------- //
            */

            // if you have a merchant-specific ClientId/ClientSecret, you need to call this method first to obtain the AccessToken:
            var accessToken = SDK.GetAccessToken("YOUR_CLIENT_ID", "YOUR_CLIENT_SECRET");

            var preference2 = new Preference
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
            preference2.Save();

            Process.Start(preference2.InitPoint);
        }
    }
}