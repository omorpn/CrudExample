using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.Helpers
{
    public class ValidationHelpers
    {
        /// <summary>
        /// Validate objects
        /// </summary>
        /// <param name="obj">Objects</param>
        /// <exception cref="ArgumentException"></exception>
        internal static void ModelValidation(Object obj)
        {

            ValidationContext validation = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validation, validationResults, true);

            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }

        }
    }
}
