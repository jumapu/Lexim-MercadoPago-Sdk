using MercadoPago;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace MercadoPagoSDK.Samples
{
    internal class CreateTestUserExample : ISample, IRequiresAccessToken
    {
        public string Name => "Create Test User";

        public string Category => "Testing";

        public void Run()
        {
            var client = new HttpClient();

            var url = $"https://api.mercadopago.com/users/test_user?access_token={SDK.AccessToken}";
            var body = JsonConvert.SerializeObject(new { site_id = "MLA" });

            var response = client.PostAsync(url, new StringContent(body)).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(result);
        }
    }
}