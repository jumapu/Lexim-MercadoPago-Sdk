using System;
using MercadoPago;
using MercadoPago.Resources;

namespace MercadoPagoSDK.Samples
{
    internal class ExceptionSample : ISample
    {
        public string Category => "Error Handling";
        public string Name => "Handling Exceptions";

        public void Run()
        {
            try
            {
                var invalid_id = 1234;
                var payment = Payment.FindById(invalid_id);
            }
            catch (MPException e) when (e.Error != null)
            {
                // Bad Request exceptions will have an Error property
                Console.WriteLine(e.Error.ToString());
            }
            catch (MPException e)
            {
                // For other exception types
                Console.WriteLine($"{e.ErrorMessage} - {e.Message}");
            }
        }
    }
}