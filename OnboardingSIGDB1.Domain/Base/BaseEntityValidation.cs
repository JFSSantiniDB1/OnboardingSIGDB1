using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using FluentValidation.Results;
using OnboardingSIGDB1.Domain.Services;

namespace OnboardingSIGDB1.Domain.Base
{
    public class BaseEntityValidation
    {
        [NotMapped]
        public bool Valid { get; private set; }
        [NotMapped]
        public bool Invalid => !Valid;
        [NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public bool Validate<TModel>(TModel model, IValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }
    }
}