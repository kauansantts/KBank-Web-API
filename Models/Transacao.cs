using KBank_Web_API.Models.Enums;
using KBank_Web_API.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KBank_Web_API.Models;

    public class Transacao
    {
        [Key]
        public int TransacaoId { get; set; }
        public DateTime DataTransacao { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Tipo da transacao é obrigatorio")]
        public TipoEnum TipoTransacao { get; set; }
        [Required(ErrorMessage = "Categoria da transacao é obrigatorio")]
        public CategoriaCompraEnum? CategoriaCompra { get; set; }
        public CategoriaEnum? CategoriaTransacao { get; set; }
        public string? Descricao { get; set; }
        [ValorTransacaoValidations]  
        [Required(ErrorMessage ="Valor da transacao é obrigatorio")]
        public double ValorTransacao { get; set; }
        public int? UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario? usuario { get; set; }
        public int? ParcelaId { get; set; }
        [JsonIgnore]
        public Parcela? Parcela { get; set; }
}
