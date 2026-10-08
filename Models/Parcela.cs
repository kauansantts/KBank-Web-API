using KBank_Web_API.Models.Enums;
using KBank_Web_API.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KBank_Web_API.Models;

public class Parcela
{
    [Key]
    public int ParcelaId { get; set; }
    public int NumeroParcela { get; set; }
    [ValorTransacaoValidations]
    public double ValorParcela { get; set; }
    public DateTime DataParcela { get; set; }
    [Required(ErrorMessage = "Tipo da parcela é obrigatorio")]
    public TipoEnum TipoParcela { get; set; } = TipoEnum.Despesa;
    [Required(ErrorMessage = "Categoria da parcela é obrigatorio")]
    public CategoriaEnum CategoriaParcela{ get; set; }
    public int CompraParceladaId { get; set; }
    public bool Processada { get; set; } = false;
    [JsonIgnore]
    public CompraParcelada compraParcelada { get; set; }

}
