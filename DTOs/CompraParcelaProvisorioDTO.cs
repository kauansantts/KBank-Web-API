using KBank_Web_API.Models.Enums;
using KBank_Web_API.Validations;
using System.ComponentModel.DataAnnotations;

namespace KBank_Web_API.DTOs
{
    public class CompraParcelaProvisorioDTO
    {
        [Required(ErrorMessage = "Nome do produto é obrigatorio!")]
        public string NomeProduto { get; set; }
        public DateTime DataCompra { get; set; }
        [ValorValidations]
        [Required(ErrorMessage = "Valor da parcela é obrigatorio")]
        public double? ValorParcela { get; set; }
        [QuantidadeParcelaValidations]
        [Required(ErrorMessage = "Quantidade da parcela é obrigatorio")]
        public int? QuantidadeParcelada { get; set; }
        [Required(ErrorMessage = "Categoria da transacao é obrigatorio")]
        public CategoriaEnum CategoriaParcela { get; set; }
    }
}
