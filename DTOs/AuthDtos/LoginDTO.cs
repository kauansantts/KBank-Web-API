using System.ComponentModel.DataAnnotations;

namespace KBank_Web_API.DTOs.AuthDtos
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Nome de usuario é obrigatorio")]
        public string NomeUsuario { get; set; } = null!;
        [Required(ErrorMessage = "Senha de usuario é obrigatorio")]
        public string Senha { get; set; } = null!;
    }
}
