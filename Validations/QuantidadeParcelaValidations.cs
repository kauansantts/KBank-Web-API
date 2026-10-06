using System.ComponentModel.DataAnnotations;

namespace KBank_Web_API.Validations
{
    public class QuantidadeParcelaValidations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            int valor = (int)value;
            if (valor <= 0)
            {
                return new ValidationResult("Quantidade de parcelas tem que ser mais de que 0");
            }

            return ValidationResult.Success;
        }
    }
}
