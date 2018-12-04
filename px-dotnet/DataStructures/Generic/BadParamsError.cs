using System;
namespace MercadoPago.DataStructures.Generic
{
    public class BadParamsError
    {
        public string Message { get; set; }

        public string Error { get; set; }

        public int Status { get; set; }

        public BadParamsCause[] Cause { get; set; }

        public override string ToString()
        {
            var message = $"Error {Status}: {Message} - {Error}\rCauses:";

            if (Cause != null)
            {
                foreach (var c in Cause)
                    message += $"\r\n  - {c.Code} - {c.Data} - {c.Description}";
            }

            return message;
        }
    }
}
