using System.Collections.Generic;
using System.Linq;

namespace MercadoPago.Validation
{
    /// <summary>
    ///Class that represents the validation results. 
    /// </summary>
    public class ValidationResult
    {
        public bool IsOk => !Errors.Any();

        public List<ValidationError> Errors { get; }

        public ValidationResult(IEnumerable<ValidationError> errors) => Errors = errors.ToList();
    }
}