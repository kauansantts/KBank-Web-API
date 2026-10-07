using KBank_Web_API.Models;
using KBank_Web_API.Models.Enums;
using KBank_Web_API.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KBank_Web_API.DTOs
{
    public class TransacaoRequestDTO
    {
        [Required(ErrorMessage = "Tipo da transacao é obrigatorio")]
        public TipoEnum TipoTransacao { get; set; }
        [Required(ErrorMessage = "Categoria da transacao é obrigatorio")]
        public CategoriaEnum CategoriaTransacao { get; set; }
        public string? Descricao { get; set; }
        [ValorTransacaoValidations]
        [Required(ErrorMessage = "Valor da transacao é obrigatorio")]
        public double ValorTransacao { get; set; }
    }
}
