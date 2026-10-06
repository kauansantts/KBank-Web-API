using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KBank_Web_API.Models;

public class Parcela
{
    [Key]
    public int ParcelaId { get; set; }
    [Required(ErrorMessage = "Valor da parcela é obrigatorio")]
    public double ValorParcela { get; set; }
    public DateTime DataParcela { get; set; }
    public int CompraParceladaId { get; set; }
    [JsonIgnore]
    public CompraParcelada compraParcelada { get; set; }
}
