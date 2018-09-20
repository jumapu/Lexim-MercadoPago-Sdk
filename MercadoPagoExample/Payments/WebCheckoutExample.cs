using System.Collections.Generic;
using System.Diagnostics;
using MercadoPago.Common;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;
using PaymentMethod = MercadoPago.DataStructures.Preference.PaymentMethod;

namespace MercadoPagoExample.Payments
{
    public static class WebCheckoutExample
    {
        public static void Run()
        {
            Utils.LoadOrPromptClientCredentials();

            // Create a preference object
            var preference = new Preference
            {
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
                },
                PaymentMethods = new PaymentMethods
                {
                    ExcludedPaymentTypes = new List<PaymentType>
                    {
                        PaymentType.Ticket,
                        PaymentType.ATM
                    },
                }
            };
            
            // save to MercadoPago
            preference.Save();

			// the InitPoint property contains the URL of the web checkout screen for this preference
            Process.Start(preference.InitPoint);
        }
    }
}
