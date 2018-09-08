using System;

namespace MercadoPago.Validation
{
    internal class ValidationException : Exception
    {
        public ValidationException(string typeName, string propertyName, Exception innerException) : 
            base($"An error occurred trying to validate property '{propertyName}' on instance of type '{typeName}'", innerException)
        {
        }
    }
}