using MercadoPago.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MercadoPagoSDK.Samples
{

    internal class CustomerSearchExample : ISample, IRequiresAccessToken
    {
        public string Name => "Search for a customer by email using a LINQ query";

        public string Category => "Customer";

        public void Run()
        {
            var accessToken = Utils.Prompt("Enter Access Token: ");

            var email = "example@user.com";

            // Traditional, stringly typed, dictionary-based search ----------------------
            var d = new Dictionary<string, string>
            {
                ["email"] = email
            };

            var customer =
                Customer.Search(d, accessToken: accessToken)
                        .FirstOrDefault();
            // ----------------------------------------------------------------------------

            customer =
                Customer.Query(accessToken: accessToken)
                        .Where(x => x.Email == email)
                        .ToList()
                        .FirstOrDefault();

            Console.WriteLine($"Customer: {customer?.Id ?? "[NOT FOUND]"}");

            if (customer == null)
            {
                customer = new Customer()
                {
                    UserAccessToken = accessToken,
                    Email = email,
                };
                customer.Save();
            }
        }
    }
}