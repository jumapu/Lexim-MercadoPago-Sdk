namespace MercadoPago.Validation
{
    /// <summary>
    /// Class that represents the Error contained in the ValidationResult class
    /// </summary>
    public class ValidationError
    {
        public const int OutOfRangeErrorCode   = 1001;
        public const int RequiredErrorCode     = 1002;
        public const int RegExpErrorCode       = 1003;
        public const int DataTypeErrorCode     = 1004;
        public const int StringLengthErrorCode = 1005;

        public int Code { get; }
        public string Message { get; }

        internal ValidationError(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}