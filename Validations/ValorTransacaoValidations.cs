using System.ComponentModel.DataAnnotations;

namespace KBank_Web_API.Validations
{
    public class ValorTransacaoValidations : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("Valor da transação é obrigatorio");
            }

            double valor = (double)value;
            if (valor <= 0)
            {
                return new ValidationResult("Existe esse tipo de valor meu querido?");
            }

            return ValidationResult.Success;
        }
    }
}
