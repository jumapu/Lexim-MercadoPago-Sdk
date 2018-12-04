using System;
namespace MercadoPago.DataStructures.Generic
{
    [Obsolete("Deprecated. Handle errors using proper try/catch instead.")]
    public interface RecuperableError
    {
        string Message { get; set; }

        string Error { get; set; }
    }
}
