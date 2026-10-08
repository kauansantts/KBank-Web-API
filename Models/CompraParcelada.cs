using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KBank_Web_API.Models.Enums;
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
    [ValorValidations]
    [Required(ErrorMessage = "Valor da parcela é obrigatorio")]
    public double ValorParcela { get; set; }
    public DateTime DataCompra { get; set; } = DateTime.Now;
    [QuantidadeParcelaValidations]
    [Required(ErrorMessage = "Quantidade da parcela é obrigatorio")]
    public int? QuantidadeParcelada { get; set; }
    public TipoEnum TipoCompra { get; set; } = TipoEnum.Despesa;
    [Required(ErrorMessage = "Categoria da compra é obrigatorio")]
    public CategoriaEnum CategoriaParcela { get; set; }

    [JsonIgnore]
    public ICollection<Parcela> Parcelas { get; set; }

    public void GerarParcelas()
    {
        for (int i = 0; i < QuantidadeParcelada; i++)
        {
            Parcelas.Add(new Parcela
            {
                CompraParceladaId = CompraParceladaId,
                ValorParcela = ValorParcela,
                DataParcela = DataCompra.AddMonths(i+1),
                NumeroParcela = i+1,
                CategoriaParcela = CategoriaParcela
            });
        }
    }
}
