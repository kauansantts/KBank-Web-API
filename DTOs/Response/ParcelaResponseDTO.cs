using KBank_Web_API.Models.Enums;
using KBank_Web_API.Validations;
using System.ComponentModel.DataAnnotations;

namespace KBank_Web_API.DTOs.Response
{
    public class ParcelaResponseDTO
    {
        [Key]
        public int ParcelaId { get; set; }
        public int NumeroParcela { get; set; }
        [ValorTransacaoValidations]
        public double ValorParcela { get; set; }
        public string DataParcela { get; set; }
        public int CompraParceladaId { get; set; }
        [Required(ErrorMessage = "Categoria da parcela é obrigatorio")]
        public TipoEnum TipoParcela { get; set; }
        [Required(ErrorMessage = "Categoria da parcela é obrigatorio")]
        public CategoriaEnum CategoriaParcela { get; set; }
        public bool Processada { get; set; }
        public string Descricao { get; set; }
    }
}
