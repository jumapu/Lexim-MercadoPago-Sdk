using System.Diagnostics;
using MercadoPago.Common;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;

namespace MercadoPagoSDK.Samples
{
    internal class UserAccessToken1: ISample
    {
        public string Name => "Per-Request Access Token #1";

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
        }
    }
}