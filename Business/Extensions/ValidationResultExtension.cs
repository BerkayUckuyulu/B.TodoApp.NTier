using Common.ResponseObjects;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions
{
    public static class ValidationResultExtension
    {
        //Data Annotation ile gelen validationResult' extent etmeyeceğiz !! FluentValidation' ile gelen ValidationResultu extent edeceğiz.
        public static List<CustomValidationError> ConvertToCustomValidationError(this ValidationResult validationResult)
        {
            List<CustomValidationError> errors = new List<CustomValidationError>();
            foreach (var item in validationResult.Errors)
            {
                errors.Add(new CustomValidationError()
                {
                    ErrorMessage = item.ErrorMessage,
                    PropertyName = item.PropertyName
                });
            }
            return errors;
        }
    }
}
