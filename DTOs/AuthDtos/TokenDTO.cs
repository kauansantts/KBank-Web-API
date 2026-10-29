using System.ComponentModel.DataAnnotations;

namespace KBank_Web_API.DTOs.AuthDtos
{
    public class TokenDTO
    {
        [Required]
        public string AccessToken { get; set; } = null!;
        [Required]
        public string RefreshToken { get; set; } = null!;
    }
}
