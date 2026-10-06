using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KBank_Web_API.Validations;

namespace KBank_Web_API.Models;

public class CompraParcelada
{
    public CompraParcelada()
    {
        Parcelas = new Collection<Parcela>();
    }

    [Key]
    public int CompraParceladaId { get; set; }
    [Required(ErrorMessage ="Nome do produto é obrigatorio!")]
    public string NomeProduto { get; set; }
    public DateTime DataCompra { get; set; } = DateTime.Now;    
    [Required(ErrorMessage = "Quantidade da parcela é obrigatorio")]
    [QuantidadeParcelaValidations]
    public int QuantidadeParcelada { get; set; }
    [JsonIgnore]
    public ICollection<Parcela> Parcelas { get; set; }
}
