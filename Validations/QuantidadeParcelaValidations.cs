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
            if (valor < 0)
            {
                return new ValidationResult("Existe quantidade de parcelas negativas meu querido?");
            }

            if(valor == 0)
            {
                return new ValidationResult("Minimo de uma parcela!");
            }

            return ValidationResult.Success;
        }
    }
}
