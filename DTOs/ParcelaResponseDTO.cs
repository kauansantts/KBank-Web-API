using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;
using KBank_Web_API.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KBank_Web_API.DTOs
{
    public class ParcelaResponseDTO
    {
        public int NumeroParcela { get; set; }
        [ValorTransacaoValidations]
        public double ValorParcela { get; set; }
        public string DataParcela { get; set; }
        public int CompraParceladaId { get; set; }
        [Required(ErrorMessage = "Categoria da parcela é obrigatorio")]
        public TipoEnum TipoParcela { get; set; }
        [Required(ErrorMessage = "Categoria da parcela é obrigatorio")]
        public CategoriaEnum CategoriaParcela { get; set; }
    }
}
