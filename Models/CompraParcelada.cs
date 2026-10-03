using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KBank_Web_API.Models;

public class CompraParcelada
{
    public CompraParcelada()
    {
        Parcelas = new Collection<Parcela>();
    }

    [Key]
    public int CompraParceladaId { get; set; }
    public string? NomeProduto { get; set; }
    public DateTime DataCompra { get; set; }
    [Required(ErrorMessage = "Quantidade da parcela é obrigatorio")]
    public int QuantidadeParcelada { get; set; }
    [JsonIgnore]
    public ICollection<Parcela> Parcelas { get; set; }
    public int TransacaoId { get; set; }
    public Transacao transacao { get; set; }
}
