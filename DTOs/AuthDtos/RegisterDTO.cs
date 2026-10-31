using System.ComponentModel.DataAnnotations;

namespace KBank_Web_API.DTOs.AuthDtos
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Nome de usuario é obrigatorio")]
        public string NomeUsuario { get; set; } = null!;
        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatorio!")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Senha de usuario é obrigatorio")]
        public string Senha { get; set; } = null!;
        public double SaldoUser { get; set; } = 0;
    }
}
