using KBank_Web_API.Models.Enums;
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

            var valor = CategoriaEnum.fixa;
            var valorRecebido = (CategoriaEnum)value;

            if (valorRecebido == valor)
            {
                return new ValidationResult("Compra parcelada so aceita livre e inesperada");
            }

            return ValidationResult.Success;
        }
    }
}
