using KBank_Web_API.Models.Enums;
using KBank_Web_API.Validations;
using System.ComponentModel.DataAnnotations;

namespace KBank_Web_API.DTOs.Request
{
    public class CompraParceladaRequestDTO
    {
        
        [Required(ErrorMessage = "Nome do produto é obrigatorio!")]
        public string Descricao { get; set; }
        [ValorTransacaoValidations]
        public double? ValorParcela { get; set; }
        [QuantidadeParcelaValidations]
        [Required(ErrorMessage = "Quantidade da parcela é obrigatorio")]
        public int? QuantidadeParcelada { get; set; }
        [Required(ErrorMessage ="Categoria da parcela é obrigatorio!")]
        public CategoriaEnum CategoriaParcela { get; set; } 
    }
}
