using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation.FluentValidation
{
    public static class ValidationTool
    {
        //validator'lar dto'lara da uygulanabileceği için object entity şeklinde tanımladık. Eğer dto'lara
        //uygulanabilme olasılığı olmasaydı IEntity entity şeklinde de tanımlayabilirdik.
        public static void Validate(IValidator validator, object entity)
        {
            var result = validator.Validate(entity);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
