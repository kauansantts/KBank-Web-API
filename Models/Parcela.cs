using KBank_Web_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KBank_Web_API.Models;

public class Parcela
{
    [Key]
    public int ParcelaId { get; set; }
    public int NumeroParcela { get; set; } 
    public double? ValorParcela { get; set; }
    public DateTime DataParcela { get; set; }
    [Required(ErrorMessage = "Categoria da parcela é obrigatorio")]
    public CategoriaEnum CategoriaCompra { get; set; }
    public int CompraParceladaId { get; set; }
    [JsonIgnore]
    public CompraParcelada compraParcelada { get; set; }

}
