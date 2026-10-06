using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KBank_Web_API.Models;

public class Usuario
{
    public Usuario()
    {
        Transacoes = new Collection<Transacao>();
    }

    [Key]
    public int UsuarioId { get; set; }
    [Required(ErrorMessage ="Primeiro nome é obrigatorio")]
    [StringLength(15)]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Sobrenome é obrigatorio")]
    [StringLength(15)]
    public string Sobrenome { get; set; }
    [Required(ErrorMessage = "Senha é obrigatorio")]
    [MinLength(6, ErrorMessage ="Minimo de 6 caracteres")]
    [StringLength(15, ErrorMessage ="Maximo de 15 caracteres")]
    public string Senha { get; set; }
    [Required(ErrorMessage = "Saldo é obrigatorio, mas pode ser 0")]
    public double SaldoAtual { get; set; }
    [JsonIgnore]
    public ICollection<Transacao> Transacoes { get; set; }
}
