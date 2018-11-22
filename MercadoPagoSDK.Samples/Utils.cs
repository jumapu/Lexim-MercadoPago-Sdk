using System;
using System.Configuration;
using MercadoPago;

namespace MercadoPagoSDK.Samples
{
    internal interface ISample
    {
        string Category { get; }
        string Name { get; }
        void Run();
    }

    internal interface IRequiresAccessToken { }

    internal interface IRequiresClientCredentials { }

    internal static class Utils
    {
        public static string Prompt(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        public static void LoadOrPromptAccessToken()
        {
            SDK.AccessToken = LoadOrPrompt(SDK.AccessToken, nameof(SDK.AccessToken), "Enter Access Token: ");
        }

        public static void LoadOrPromptClientCredentials()
        {
            SDK.ClientId     = LoadOrPrompt(SDK.ClientId,     nameof(SDK.ClientId),     "Enter Client Id: ");
            SDK.ClientSecret = LoadOrPrompt(SDK.ClientSecret, nameof(SDK.ClientSecret), "Enter Client Secret: ");
            SDK.AppId        = LoadOrPrompt(SDK.AppId,        nameof(SDK.AppId),        "Enter App Id: ");
        }

        private static string LoadOrPrompt(string currentValue, string name, string prompt)
        {
            while (true)
            {
                var value = currentValue;
                if (!string.IsNullOrEmpty(value))
                    return value;
                value = Environment.GetEnvironmentVariable(name);
                if (!string.IsNullOrEmpty(value))
                    return value;

                value = Prompt(prompt);
                if (!string.IsNullOrEmpty(value))
                {
                    Environment.SetEnvironmentVariable(name, value);
                    return value;
                }
            }
        }
    }
}