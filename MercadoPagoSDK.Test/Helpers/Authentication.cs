using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MercadoPago;
using Newtonsoft.Json.Linq;

namespace MercadoPagoSDK.Test
{
    public static class Authentication
    {
        public static void Initialize(bool useAccessToken, bool useClientCredentials)
        {
            // Avoid SSL Cert error
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol |= (SecurityProtocolType)3072;

            // Make a Clean Test
            SDK.CleanConfiguration();
            SDK.SetBaseUrl("https://api.mercadopago.com");

            if (useAccessToken)
            {
                SDK.AccessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
            }

            if (useClientCredentials)
            {
                SDK.ClientId = Environment.GetEnvironmentVariable("CLIENT_ID");
                SDK.ClientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
            }
        }

    }
}
