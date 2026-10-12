using System.ComponentModel.DataAnnotations;

namespace KBank_Web_API.Validations
{
    public class CategoriaCompraValidations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            if (value == "fixa")
            {
                return new ValidationResult("Compra parcelada so aceita livre e inesperada");
            }

            return ValidationResult.Success;
        }
    }
}
