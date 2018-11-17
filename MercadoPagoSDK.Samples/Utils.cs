using System;
using System.Configuration;
using MercadoPago;

namespace MercadoPagoSDK.Samples
{
    internal static class Utils
    {
        public static string Prompt(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        public static void SetEnvVarsFromAppConfig()
        {
            SetIfNotExist("ACCESS_TOKEN", ConfigurationManager.AppSettings["ACCESS_TOKEN"]);
            SetIfNotExist("CLIENT_ID", ConfigurationManager.AppSettings["CLIENT_ID"]);
            SetIfNotExist("CLIENT_SECRET", ConfigurationManager.AppSettings["CLIENT_SECRET"]);

        }

        private static void SetIfNotExist(string key, string value)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key))) {
                Environment.SetEnvironmentVariable(key, value);
            };
        }

        public static void LoadOrPromptAccessToken()
        {
            SDK.AccessToken = LoadOrPrompt(SDK.AccessToken, nameof(SDK.AccessToken), "Ingrese Access Token: ");
        }

        public static void LoadOrPromptClientCredentials()
        {
            SDK.ClientId = LoadOrPrompt(SDK.ClientId, nameof(SDK.ClientId), "Ingrese Client Id: ");
            SDK.ClientSecret = LoadOrPrompt(SDK.ClientSecret, nameof(SDK.ClientSecret), "Ingrese Client Secret: ");
            SDK.AppId = LoadOrPrompt(SDK.AppId, nameof(SDK.AppId), "Ingrese App Id: ");
        }

        private static string LoadOrPrompt(string currentValue, string name, string prompt)
        {
            while (true)
            {
                var value = currentValue;
                if (!string.IsNullOrEmpty(value))
                    return value;
                value = ConfigurationManager.AppSettings[name];
                if (!string.IsNullOrEmpty(value))
                    return value;
                value = Environment.GetEnvironmentVariable(name);
                if (!string.IsNullOrEmpty(value))
                    return value;
                value = Prompt(prompt);
                if (!string.IsNullOrEmpty(value))
                    return value;
            }
        }
    }
}