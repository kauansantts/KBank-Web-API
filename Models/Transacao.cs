using KBank_Web_API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KBank_Web_API.Models;

public class Transacao
{
    [Key]
    public int TransacaoId { get; set; }
    public DateTime DataTransacao { get; set; }
    public CategoriaEnum CategoriaTransacao { get; set; }
    public TipoEnum TipoTransacao { get; set; }
    public string? Descricao { get; set; }
    public double ValorTransacao { get; set; }
    public int UsuarioId { get; set; }
    [JsonIgnore]
    public Usuario? usuario { get; set; }
}
